namespace Domain
{
    public struct RoomCleaned : IDomainEvent, IRoomDomainEvent
    {
        public RoomCleaned(RoomId roomId)
        {
            RoomId = roomId;
        }

        public RoomId RoomId { get; }
    }
}