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
        private static readonly DateTime Horodate = new DateTime(2019, 9, 11, 16, 15, 0);
        private readonly string _filesPath;

        public EventStoreShould()
        {
            _filesPath = Path.Combine(Directory.GetCurrentDirectory(), "TestsEvents");
            if (Directory.Exists(_filesPath))
            {
                foreach (var file in Directory.GetFiles(_filesPath, $"*.json"))
                {
                    File.Delete(file);
                }   
            }
        }
        
        [Fact]
        public async Task PersistEventsInFiles()
        {
            var fileName = "1568218500000-roomCheckedAsOk.json";
            Check.That(File.Exists(fileName)).IsFalse();
            
            var eventStore = CreateEventStore();
            var domainEvent = new RoomCheckedAsOk(new RoomId("whatever"));
            await eventStore.Append(domainEvent);

            var filePath = Path.Combine(_filesPath, fileName);
            Check.That(File.Exists(filePath)).IsTrue();
            var fileContent = File.ReadAllText(filePath);
            Check.That(fileContent).Contains("whatever");
        }
        
        [Fact]
        public async Task PersistAndReadEvents()
        {
            var domainEvent1 = new RoomCheckedAsOk(new RoomId("whatever"));
            var horodateEvent1 = Horodate;
            await CreateEventStore(horodateEvent1).Append(domainEvent1);
            
            var domainEvent2 = new RoomDamageReported(new RoomId("dude"), "drawings on walls");
            var horodateEvent2 = Horodate.AddSeconds(1);
            await CreateEventStore(horodateEvent2).Append(domainEvent2);

            var domainEvent3 = new RoomCheckedAsOk(new RoomId("dady cool"));
            var horodateEvent3 = Horodate.AddSeconds(-1);
            await CreateEventStore(horodateEvent3).Append(domainEvent3);

            var domainEvent4 = new GuestCheckedOut(new RoomId("anyway"));
            var horodateEvent4 = Horodate.AddSeconds(-2);
            await CreateEventStore(horodateEvent4).Append(domainEvent4);


            var history = await CreateEventStore().GetAggregateHistory();
            Check.That(history).ContainsExactly(domainEvent4, domainEvent3, domainEvent1, domainEvent2);
        }

        private App.EventStore.EventStore CreateEventStore(DateTime? horodate = null)
        {
            return new App.EventStore.EventStore(
                _filesPath,
                () => horodate ?? Horodate);
        }
    }
}