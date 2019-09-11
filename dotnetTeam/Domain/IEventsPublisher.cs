using System.Threading.Tasks;

namespace Domain
{
    public interface IEventsPublisher
    {
        Task Publish(IDomainEvent evt);
    }
}