using System.Threading.Tasks;

namespace Domain
{
    public interface IEventStore
    {
        Task Append(IDomainEvent domainEvent);

        Task<IDomainEvent[]> GetAggregateHistory();
    }
}