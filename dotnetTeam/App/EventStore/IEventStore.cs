using System.Threading.Tasks;
using Domain;

namespace App.EventStore
{
    public interface IEventStore
    {
        Task Append(IDomainEvent domainEvent);

        Task<IDomainEvent[]> GetAggregateHistory();
    }
}