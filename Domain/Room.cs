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
            publisher.Publish(new RoomCheckedAsOk(_roomId, status));
        }
    }

    public struct RoomCheckedAsOk : IDomainEvent
    {
        public RoomCheckStatus Status { get; }

        public RoomCheckedAsOk(RoomId expectedRoomId, RoomCheckStatus status)
        {
            Status = status;
        }
    }
}