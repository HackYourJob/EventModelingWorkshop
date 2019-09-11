namespace Domain
{
    public struct GuestCheckedOut : IDomainEvent
    {
        public RoomId RoomId { get; }

        public GuestCheckedOut(RoomId roomId)
        {
            RoomId = roomId;
        }
    }
}