using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Domain;

namespace FootNotes.Annotations.Domain.TagModels
{
    public class Tag(string name, bool isDefault, Guid? userId) : Entity, IAggregateRoot
    {
        public string Name { get; private set; } = name;
        public bool IsDefault { get; private set; } = isDefault;
        public Guid? UserId { get; private set; } = userId;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public override void ThrowIfInvalid()
        {
            throw new NotImplementedException();
        }
    }

}
