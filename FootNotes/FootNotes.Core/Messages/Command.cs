using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FootNotes.Core.Messages
{
    public abstract class Command: Message, IRequest<MessageResponse>
    {
        public abstract bool IsValid(out string msg);
    }
}
