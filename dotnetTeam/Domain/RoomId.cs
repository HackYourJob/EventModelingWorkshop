using System;

namespace Domain
{
    public struct RoomId
    {
        public string Value { get; }

        public RoomId(string value)
        {
            Value = value;
        }
    }
}