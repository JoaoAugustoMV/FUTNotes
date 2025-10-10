using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Core.Domain
{
    public class EntityInvalidException : Exception
    {
        public EntityInvalidException()
        { }

        public EntityInvalidException(string message) : base(message)
        { }
        
    }
}
