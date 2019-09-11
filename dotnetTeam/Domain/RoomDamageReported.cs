namespace Domain
{
    public struct RoomDamageReported : IDomainEvent
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