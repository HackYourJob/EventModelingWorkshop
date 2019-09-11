using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using NFluent;
using Xunit;

namespace DomainShould
{
    public class RoomShould
    {
        [Fact]
        public async Task RaiseRoomCheckedAsOkWhenCheckingDoneOk()
        {
            var publisher = new FakePublisher();
            var expectedRoomId = new RoomId("101");
            var room = new Room(expectedRoomId);
            await room.CheckingDone(publisher, RoomCheckStatus.Ok);

            Check.That(publisher.Events).Contains(new RoomCheckedAsOk(expectedRoomId));
        }
        
        [Fact]
        public async Task RaiseRoomCheckedAsKoWhenCheckingDoneIsNotOk()
        {
            var publisher = new FakePublisher();
            var expectedRoomId = new RoomId("101");
            var room = new Room(expectedRoomId);
            await room.CheckingDone(publisher, RoomCheckStatus.Ko);

            Check.That(publisher.Events).Contains(new RoomCheckedAsKo(expectedRoomId));
        }

        [Fact]
        public void RaiseRoomCleaningRequestedWhenRequestClean()
        {
            var publisher = new FakePublisher();
            var expectedRoomId = new RoomId("101");
            var room = new Room(expectedRoomId);
            room.RequestClean(publisher);

            Check.That(publisher.Events).Contains(new RoomCleaningRequested(expectedRoomId));
        }
    }

    public class FakePublisher : IEventsPublisher
    {
        public IList<IDomainEvent> Events { get; } = new List<IDomainEvent>();
        
        public Task Publish(IDomainEvent evt)
        {
            Events.Add(evt);
            return Task.CompletedTask;
        }
    }
}