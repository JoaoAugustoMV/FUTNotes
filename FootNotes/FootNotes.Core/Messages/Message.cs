using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UUIDNext;

namespace FootNotes.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }
        public DateTime Timestamp { get; private set; }

        protected Message()
        {
            MessageType = GetType().Name;            
            Timestamp = DateTime.UtcNow;
        }
    }
}
