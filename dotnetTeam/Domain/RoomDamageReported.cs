namespace Domain
{
    public struct RoomDamageReported : IDomainEvent, IRoomDomainEvent
	{
        public RoomId RoomId { get; }
        public string DamageDescription { get; }

        public RoomDamageReported(RoomId roomId, string damageDescription)
        {
            RoomId = roomId;
            DamageDescription = damageDescription;
        }
    }
}