using System.Collections.Generic;
using Domain;

namespace DomainShould
{
    public class FakePublisher : IEventsPublisher
    {
        public IList<IDomainEvent> Events { get; } = new List<IDomainEvent>();
        
        public void Publish(IDomainEvent evt)
        {
            Events.Add(evt);
        }
    }
}