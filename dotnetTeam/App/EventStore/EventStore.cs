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
            ["roomCheckedAsOk"] = typeof(RoomCheckedAsOk),
            ["roomDamageReported"] = typeof(RoomDamageReported),
            ["roomCleaningRequested"] = typeof(RoomCleaningRequested),
            ["guestCheckedOut"] = typeof(GuestCheckedOut),
        };
        
        private static readonly IDictionary<Type, string> MappingEventTypeToKey = MappingKeyToEventType.ToDictionary(x => x.Value, x => x.Key);

        private long ToMsUnixTimeStamp(DateTime horodate)
        {
            var unixOriginDate = new DateTime(1970, 1 ,1);
            return (long) horodate.Subtract(unixOriginDate).TotalMilliseconds;
        }

        public Task<IDomainEvent[]> GetAggregateHistory()
        {
            const string eventFilePattern = "(\\d+)\\-(.*).json";
            var readFiles = Directory.EnumerateFiles(_directory)
                .Where(filePath => Regex.IsMatch(filePath, eventFilePattern))
                .Select(filePath =>
                {
                    var match = Regex.Match(Path.GetFileName(filePath), eventFilePattern);
                    var horodate = long.Parse(match.Groups[1].Value);
                    var eventTypeTag = match.Groups[2].Value;
                    if (MappingKeyToEventType.TryGetValue(eventTypeTag, out var eventType))
                        return (horodate: horodate, filePath: filePath, eventType: eventType);
                    return (horodate: horodate, filePath: filePath, eventType: null);
                })
                .Where(t => t.eventType != default(Type))
                .OrderBy(t => t.horodate)
                .Select(async t =>
                {
                    var payload = await File.ReadAllTextAsync(t.filePath);
                    var domainEvent = (IDomainEvent)JsonConvert.DeserializeObject(payload, t.eventType);
                    return domainEvent;
                });

            return Task.WhenAll(readFiles);
        }
    }
}