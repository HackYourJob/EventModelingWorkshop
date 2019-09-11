namespace Domain
{
    public class Room
    {
        public static Room Create()
        {
            throw new System.NotImplementedException();
        }

        public void CheckingDone(object ok)
        {
            throw new System.NotImplementedException();
        }
    }

    public struct RoomCheckedAsOk : IDomainEvent
    {
        public RoomCheckStatus Status { get; }

        public RoomCheckedAsOk(RoomId expectedRoomId, RoomCheckStatus status)
        {
            Status = status;
        }
    }
}