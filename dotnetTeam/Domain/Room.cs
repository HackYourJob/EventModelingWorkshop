namespace Domain
{
    public class Room
    {
        private readonly RoomId _roomId;

        public Room(RoomId roomId)
        {
            _roomId = roomId;
        }

        public void CheckingDone(IEventsPublisher publisher, RoomCheckStatus status)
        {
            if (status == RoomCheckStatus.Ok)
            {
                publisher.Publish(new RoomCheckedAsOk(_roomId));
            }
            else
            {
                publisher.Publish(new RoomCheckedAsKo(_roomId));
            }
        }
    }
}