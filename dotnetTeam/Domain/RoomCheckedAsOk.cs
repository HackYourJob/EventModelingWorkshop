namespace Domain
{
    public struct RoomCheckedAsOk : IDomainEvent, IRoomDomainEvent
	{
        public RoomId RoomId { get; }

        public RoomCheckedAsOk(RoomId roomId)
        {
            RoomId = roomId;
        }
    }
}