namespace Domain
{
    public struct RoomCheckedIn : IDomainEvent, IRoomDomainEvent
    {
        public RoomCheckedIn(RoomId roomId)
        {
            RoomId = roomId;
        }

        public RoomId RoomId { get; }
    }
}