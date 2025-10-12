using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.IAM.Domain;

namespace FootNotes.IAM.Application.Events
{
    public class UserModifiedEvent(Guid id, string userName, string email, DateTime createdAt, UserType userType) : Event(id)
    {
        public Guid Id { get; private set; } = id;
        public string Username { get; private set; } = userName;
        public string Email { get; private set; } = email;        
        public DateTime CreatedAt { get; private set; } = createdAt;
        public UserType UserType { get; private set; } = userType;
    }
}
