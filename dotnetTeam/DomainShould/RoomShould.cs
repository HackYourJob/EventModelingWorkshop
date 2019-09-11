using System.Collections.Generic;
using Domain;
using NFluent;
using Xunit;

namespace DomainShould
{
    public class RoomShould
    {
        [Fact]
        public void RaiseRoomCheckedAsOkWhenCheckingDoneOk()
        {
            var publisher = new FakePublisher();
            var expectedRoomId = new RoomId("101");
            var room = new Room(expectedRoomId);
            room.CheckingDone(publisher, RoomCheckStatus.Ok);

            Check.That(publisher.Events).Contains(new RoomCheckedAsOk(expectedRoomId));
        }
        
        [Fact]
        public void RaiseRoomCheckedAsKoWhenCheckingDoneIsNotOk()
        {
            var publisher = new FakePublisher();
            var expectedRoomId = new RoomId("101");
            var room = new Room(expectedRoomId);
            room.CheckingDone(publisher, RoomCheckStatus.Ko);

            Check.That(publisher.Events).Contains(new RoomCheckedAsKo(expectedRoomId));
        }
    }

    public class FakePublisher : IEventsPublisher
    {
        public IList<IDomainEvent> Events { get; } = new List<IDomainEvent>();
        
        public void Publish(IDomainEvent evt)
        {
            Events.Add(evt);
        }
    }
}