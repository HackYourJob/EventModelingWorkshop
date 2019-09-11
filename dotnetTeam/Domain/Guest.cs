using System.Threading.Tasks;

namespace Domain
{
    public class Guest
    {
        public Task Checkout(IEventsPublisher publisher, RoomId roomId)
        {
            return publisher.Publish(new GuestCheckedOut(roomId));
        }
    }
}