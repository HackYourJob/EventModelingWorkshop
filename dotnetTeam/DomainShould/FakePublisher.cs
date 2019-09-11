using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace DomainShould
{
    public class FakePublisher : IEventsPublisher
    {
        public IList<IDomainEvent> Events { get; } = new List<IDomainEvent>();
        
        public Task Publish(IDomainEvent evt)
        {
            Events.Add(evt);
            return Task.CompletedTask;
        }
    }
}