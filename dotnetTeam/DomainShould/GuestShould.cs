using System.Threading.Tasks;
using Domain;
using NFluent;
using Xunit;

namespace DomainShould
{
    public class GuestShould
    {
        private static readonly RoomId ExpectedRoomId = new RoomId("101");
        private readonly FakePublisher _publisher = new FakePublisher();

        [Fact]
        public async Task RaisedGuestCheckedOutWhenCheckingOutGuest()
        {
            var guest = new Guest();
            await guest.Checkout(_publisher, ExpectedRoomId);

            Check.That(_publisher.Events).Contains(new GuestCheckedOut(ExpectedRoomId));
        }
    }
}