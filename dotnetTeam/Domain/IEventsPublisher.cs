namespace Domain
{
    public interface IEventsPublisher
    {
        void Publish(IDomainEvent evt);
    }
}