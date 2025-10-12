using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Core.Messages
{
    public class CommandResponse : MessageResponse
    {
        public Guid AggregateId { get; private set; }
        public CommandResponse(Guid id, bool sucess) : base(sucess)
        {
            AggregateId = id;
        }

        public CommandResponse(Guid id, bool sucess, string msg) : base(sucess, msg)
        {
            AggregateId = id;
        }
    }
}
