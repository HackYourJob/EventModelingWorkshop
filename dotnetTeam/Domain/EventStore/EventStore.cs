using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Domain
{
    public sealed class EventStore : IEventStore
    {
        private readonly string _directory;
        private readonly Func<DateTime> _getHorodate;

        public EventStore(string directory) : this(directory, () => DateTime.Now)  { }
        
        public EventStore(string directory, Func<DateTime> getHorodate)
        {
            _directory = directory;
            _getHorodate = getHorodate;
        }
        
        public Task Append(IDomainEvent domainEvent)
        {
            var fileName = $"{_getHorodate():yyyy-MM-dd-HH-mm-ss}-{domainEvent.GetType().Name}.json";
            var payload = JsonConvert.SerializeObject(domainEvent);
            return File.WriteAllTextAsync(Path.Combine(_directory, fileName), payload);
        }

        public Task<IDomainEvent[]> GetAggregateHistory()
        {
            var readFiles = Directory.EnumerateFiles(_directory)
                .Where(filePath => Regex.IsMatch(filePath, "(.*)\\-([^\\-]*).json"))
                .Select(async filePath =>
                {
                    var match = Regex.Match(Path.GetFileName(filePath), "(.*)\\-([^\\-]*).json");
                    var eventType = Type.GetType($"Domain.{match.Groups[2].Value}");
                    var payload = await File.ReadAllTextAsync(filePath);
                    return (IDomainEvent) JsonConvert.DeserializeObject(payload, eventType);
                });

            return Task.WhenAll(readFiles);
        }
    }
}