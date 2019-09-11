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

    public struct RoomCheckedAsKo : IDomainEvent
    {
        public RoomCheckedAsKo(RoomId roomId)
        {
            RoomId = roomId;
        }

        public RoomId RoomId { get; }
    }
}