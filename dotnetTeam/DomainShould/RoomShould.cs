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
            await room.CheckingDone(_publisher);

            Check.That(_publisher.Events).Contains(new RoomCheckedAsOk(ExpectedRoomId));
        }
        
        [Fact]
        public async Task RaiseRoomDamageReportedWhenReportDamage()
        {
            var room = new Room(ExpectedRoomId);
            var content = "content";
            await room.ReportDamage(_publisher, content);

            Check.That(_publisher.Events).Contains(new RoomDamageReported(ExpectedRoomId, content));
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