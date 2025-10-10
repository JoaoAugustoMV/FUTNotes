using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Core.Data.EventSourcing
{
    public class StoredEvent(Guid id, string type, DateTime timestamp, string data)
    {
        public Guid Id { get; private set; } = id;

        public string Type { get; private set; } = type;

        public DateTime Timestamp { get; set; } = timestamp;

        public string Data { get; private set; } = data;
    }
}
