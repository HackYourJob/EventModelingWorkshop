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
            var room = Room.Create();
            room.CheckingDone(RoomCheckStatus.Ok);

            Check.That(publisher.Events).Contains(new RoomCheckedAsOk(expectedRoomId, RoomCheckStatus.Ok));
        }
    }

    public class FakePublisher : IEventsPublisher
    {
        public IList<IDomainEvent> Events { get; }
    }
}