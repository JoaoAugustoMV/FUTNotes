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
        public Guid Id { get; private set; }

        public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

        protected Entity()
        {
            Id = Uuid.NewSequential();
        }

        /// <summary>
        /// Must be implemented in derived classes to validate the entity's state. 
        /// </summary>
        /// <remarks>
        /// If the entity is invalid, it should throw an exception and provide an error message.
        /// 
        /// </remarks>
        /// 
        /// <param name="msg"></param>
        /// <returns></returns>
        public abstract void ThrowIfInvalid();

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
