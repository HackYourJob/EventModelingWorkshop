namespace Domain
{
    public struct RoomCheckedAsOk : IDomainEvent
    {
        public RoomId RoomId { get; }

        public RoomCheckedAsOk(RoomId roomId)
        {
            RoomId = roomId;
        }
    }
}