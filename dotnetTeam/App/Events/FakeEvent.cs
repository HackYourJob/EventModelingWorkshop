using System;
using App.EventStore;

namespace App.Events
{
    public class FakeEvent : IDomainEvent, IEquatable<FakeEvent>
    {
        public string Field1 { get; }
        
        public int Field2 { get; }

        public FakeEvent(string field1, int field2)
        {
            Field1 = field1;
            Field2 = field2;
        }

        public bool Equals(FakeEvent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Field1 == other.Field1 && Field2 == other.Field2;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FakeEvent) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Field1 != null ? Field1.GetHashCode() : 0) * 397) ^ Field2;
            }
        }
    }
}