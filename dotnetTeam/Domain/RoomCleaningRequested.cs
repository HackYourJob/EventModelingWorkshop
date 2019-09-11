namespace Domain
{
    public struct RoomCleaningRequested : IDomainEvent
    {
        public RoomId RoomId { get; }

        public RoomCleaningRequested(RoomId roomId)
        {
            RoomId = roomId;
        }
    }
}