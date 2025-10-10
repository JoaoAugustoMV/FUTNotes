using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.IAM.Domain;

namespace FootNotes.IAM.Application.Events
{
    public class UserModifiedEvent: Event
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserType UserType { get; set; }

        public UserModifiedEvent(Guid Id, EventCRUDType eventType): base(Id, eventType)
        {                        
        }
    }
}
