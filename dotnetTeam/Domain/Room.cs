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

        public Task CheckingDone(IEventsPublisher publisher)
        {
                return publisher.Publish(new RoomCheckedAsOk(_roomId));
        }

        public Task RequestClean(IEventsPublisher publisher)
        {
            return publisher.Publish(new RoomCleaningRequested(_roomId));
        }

        public Task ReportDamage(IEventsPublisher publisher, string content)
        {
           return publisher.Publish(new RoomDamageReported(_roomId, content));
        }
    }
}