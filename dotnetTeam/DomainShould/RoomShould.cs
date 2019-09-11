using System.Threading.Tasks;
using Domain;
using NFluent;
using Xunit;

namespace DomainShould
{
    public class RoomShould
    {
        private static readonly RoomId ExpectedRoomId = new RoomId("101");
        
        private readonly FakePublisher _publisher = new FakePublisher();

        [Fact]
        public async Task RaiseRoomCheckedAsOkWhenCheckingDoneOk()
        {
            var room = new Room(ExpectedRoomId);
            await room.CheckingDone(_publisher, RoomCheckStatus.Ok);

            Check.That(_publisher.Events).Contains(new RoomCheckedAsOk(ExpectedRoomId));
        }
        
        [Fact]
        public async Task RaiseRoomCheckedAsKoWhenCheckingDoneIsNotOk()
        {
            var room = new Room(ExpectedRoomId);
            await room.CheckingDone(_publisher, RoomCheckStatus.Ko);

            Check.That(_publisher.Events).Contains(new RoomDamageReported(ExpectedRoomId, string.Empty));
        }

        [Fact]
        public async Task RaiseRoomCleaningRequestedWhenRequestClean()
        {
            var room = new Room(ExpectedRoomId);
            await room.RequestClean(_publisher);

            Check.That(_publisher.Events).Contains(new RoomCleaningRequested(ExpectedRoomId));
        }
    }
}