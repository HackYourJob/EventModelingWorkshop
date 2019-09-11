using System.Threading.Tasks;

namespace Domain
{
    public class Room
    {
        private readonly RoomId _roomId;

        public Room(RoomId roomId)
        {
            _roomId = roomId;
        }

        public Task CheckingDone(IEventsPublisher publisher, RoomCheckStatus status)
        {
            if (status == RoomCheckStatus.Ok)
            {
                return publisher.Publish(new RoomCheckedAsOk(_roomId));
            }
            else
            {
                return publisher.Publish(new RoomDamageReported(_roomId, string.Empty));
            }
        }

        public Task RequestClean(IEventsPublisher publisher)
        {
            return publisher.Publish(new RoomCleaningRequested(_roomId));
        }
    }
}