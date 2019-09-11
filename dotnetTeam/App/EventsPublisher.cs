using System.Threading.Tasks;
using App.EventStore;
using Domain;

namespace App
{
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