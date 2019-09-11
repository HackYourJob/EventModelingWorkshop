using System.Threading.Tasks;

namespace Domain
{
    public interface IEventsPublisher
    {
        Task Publish(IDomainEvent evt);
    }

    public class EventsPublisher : IEventsPublisher
    {
        private readonly IEventStore _eventStore;

        public EventsPublisher(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }


        public Task Publish(IDomainEvent evt) => _eventStore.Append(evt);
    }
}