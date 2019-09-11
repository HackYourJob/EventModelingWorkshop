using System;
using System.IO;
using System.Threading.Tasks;
using Domain;
using Xunit;
using NFluent;

namespace Tests.EventStore
{
    public class EventStoreShould
    {
        private static readonly DateTime Horodate = DateTime.Now;

        public EventStoreShould()
        {
            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory(), $"*RoomCheckedAs*.json"))
            {
                File.Delete(file);
            }
        }
        
        [Fact]
        public async Task PersistEventsInFiles()
        {
            var fileName = $"{Horodate:yyyy-MM-dd-HH-mm-ss}-{nameof(RoomCheckedAsOk)}.json";
            Check.That(File.Exists(fileName)).IsFalse();
            
            var eventStore = CreateEventStore();
            var domainEvent = new RoomCheckedAsOk(new RoomId("whatever"));
            await eventStore.Append(domainEvent);

            Check.That(File.Exists(fileName)).IsTrue();
            var fileContent = File.ReadAllText(fileName);
            Check.That(fileContent).Contains("whatever");
        }
        
        [Fact]
        public async Task PersistAndReadEvents()
        {
            var domainEvent1 = new RoomCheckedAsOk(new RoomId("whatever"));
            var horodateEvent1 = Horodate;
            await CreateEventStore(horodateEvent1).Append(domainEvent1);
            
            var domainEvent2 = new RoomDamageReported(new RoomId("dude"), "toto");
            var horodateEvent2 = Horodate.AddSeconds(1);
            await CreateEventStore(horodateEvent2).Append(domainEvent2);

            var domainEvent3 = new RoomCheckedAsOk(new RoomId("dady cool"));
            var horodateEvent3 = Horodate.AddSeconds(-1);
            await CreateEventStore(horodateEvent3).Append(domainEvent3);

            var history = await CreateEventStore().GetAggregateHistory();
            Check.That(history).ContainsExactly(domainEvent3, domainEvent1, domainEvent2);
        }

        private static Domain.EventStore CreateEventStore(DateTime? horodate = null)
        {
            return new Domain.EventStore(
                Directory.GetCurrentDirectory(),
                () => horodate ?? Horodate);
        }
    }
}