using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain;
using Newtonsoft.Json;

namespace App.EventStore
{
    public sealed class EventStore : IEventStore
    {
        private readonly string _directory;
        private readonly Func<DateTime> _getHorodate;

        public EventStore(string directory) : this(directory, () => DateTime.Now)  { }
        
        public EventStore(string directory, Func<DateTime> getHorodate)
        {
            _directory = directory;
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            _getHorodate = getHorodate;
        }
        
        public Task Append(IDomainEvent domainEvent)
        {
            var typeKey = MappingEventTypeToKey[domainEvent.GetType()];
            
            var fileName = $"{ToMsUnixTimeStamp(_getHorodate())}-{typeKey}.json";
            var payload = JsonConvert.SerializeObject(domainEvent);
            return File.WriteAllTextAsync(Path.Combine(_directory, fileName), payload);
        }
        
        private static readonly IDictionary<string, Type> MappingKeyToEventType = new Dictionary<string, Type>()
        {
            ["room-checked-as-ok"] = typeof(RoomCheckedAsOk),
            ["room-damage-reported"] = typeof(RoomDamageReported),
            ["room-cleaning-requested"] = typeof(RoomCleaningRequested)
        };
        
        private static readonly IDictionary<Type, string> MappingEventTypeToKey = MappingKeyToEventType.ToDictionary(x => x.Value, x => x.Key);

        private long ToMsUnixTimeStamp(DateTime horodate)
        {
            var _1970 = new DateTime(1970, 1 ,1);
            return (long) horodate.Subtract(_1970).TotalMilliseconds;
        }

        public Task<IDomainEvent[]> GetAggregateHistory()
        {
            const string eventFilePattern = "(\\d+)\\-(.*).json";
            var readFiles = Directory.EnumerateFiles(_directory)
                .Where(filePath => Regex.IsMatch(filePath, eventFilePattern))
                .Select(async filePath =>
                {
                    var match = Regex.Match(Path.GetFileName(filePath), eventFilePattern);
                    if (MappingKeyToEventType.TryGetValue(match.Groups[2].Value, out var eventType))
                    {
                        var payload = await File.ReadAllTextAsync(filePath);
                        return (IDomainEvent) JsonConvert.DeserializeObject(payload, eventType);
                    }

                    return null;
                })
                .Where(evt => evt != null);

            return Task.WhenAll(readFiles);
        }
    }
}