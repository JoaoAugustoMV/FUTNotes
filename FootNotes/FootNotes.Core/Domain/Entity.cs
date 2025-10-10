using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using UUIDNext;

namespace FootNotes.Core.Domain
{
    public abstract class Entity
    {
        private List<Event> _events = [];
        public Guid Id { get; set; }

        public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

        protected Entity()
        {
            Id = Uuid.NewSequential();
        }

        public abstract bool IsValid(out string msg);

        public void AddEvent(Event evento)
        {
            _events = _events ?? [];
            _events.Add(evento);
        }

        public void RemoveEvent(Event eventItem)
        {
            _events?.Remove(eventItem);
        }

        public void ClearEvents()
        {
            _events?.Clear();
        }
    }
}
