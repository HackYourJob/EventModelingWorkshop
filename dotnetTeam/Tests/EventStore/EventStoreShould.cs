using System;
using System.IO;
using System.Threading.Tasks;
using App.Events;
using Xunit;
using NFluent;

namespace Tests.EventStore
{
    public class EventStoreShould
    {
        private static readonly DateTime Horodate = DateTime.Now;

        public EventStoreShould()
        {
            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory(), $"*{nameof(FakeEvent)}.json"))
            {
                File.Delete(file);
            }
        }
        
        [Fact]
        public async Task PersistEventsInFiles()
        {
            var fileName = $"{Horodate:yyyy-MM-dd-HH-mm-ss}-{nameof(FakeEvent)}.json";
            Check.That(File.Exists(fileName)).IsFalse();
            
            var eventStore = CreateEventStore();
            var domainEvent = new FakeEvent("whatever", 5);
            await eventStore.Append(domainEvent);

            Check.That(File.Exists(fileName)).IsTrue();
            var fileContent = File.ReadAllText(fileName);
            Check.That(fileContent).Contains("whatever", "5");
        }
        
        [Fact]
        public async Task PersistAndReadEvents()
        {
            var domainEvent1 = new FakeEvent("whatever", 5);
            var horodateEvent1 = Horodate;
            await CreateEventStore(horodateEvent1).Append(domainEvent1);
            
            var domainEvent2 = new FakeEvent("dude", 8);
            var horodateEvent2 = Horodate.AddSeconds(1);
            await CreateEventStore(horodateEvent2).Append(domainEvent2);

            var domainEvent3 = new FakeEvent("dady cool", 24);
            var horodateEvent3 = Horodate.AddSeconds(-1);
            await CreateEventStore(horodateEvent3).Append(domainEvent3);

            var history = await CreateEventStore().GetAggregateHistory();
            Check.That(history).ContainsExactly(domainEvent3, domainEvent1, domainEvent2);
        }

        private static App.EventStore.EventStore CreateEventStore(DateTime? horodate = null)
        {
            return new App.EventStore.EventStore(
                Directory.GetCurrentDirectory(),
                () => horodate ?? Horodate);
        }
    }
}