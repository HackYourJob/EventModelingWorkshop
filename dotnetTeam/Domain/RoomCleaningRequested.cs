namespace Domain
{
    public struct RoomCleaningRequested : IDomainEvent, IRoomDomainEvent
	{
        public RoomId RoomId { get; }

        public RoomCleaningRequested(RoomId roomId)
        {
            RoomId = roomId;
        }
    }
}