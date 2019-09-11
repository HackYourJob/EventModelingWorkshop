namespace Domain
{
    public struct RoomCleaningRequested : IDomainEvent
    {
        private readonly RoomId _roomId;

        public RoomCleaningRequested(RoomId roomId)
        {
            _roomId = roomId;
        }
    }
}