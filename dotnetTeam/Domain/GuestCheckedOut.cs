namespace Domain
{
    public struct GuestCheckedOut : IDomainEvent, IRoomDomainEvent
    {
        public RoomId RoomId { get; }

        public GuestCheckedOut(RoomId roomId)
        {
            RoomId = roomId;
        }
    }

	public interface IRoomDomainEvent
	{
		RoomId RoomId { get ; }
	}
}