using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Core.Messages
{
    public class MessageResponse
    {
        public bool Sucess { get; set; }
        public string? Message { get; set; }

        public MessageResponse(bool sucess, string msg)
        {
            Sucess = sucess;
            Message = msg;
        }

        public MessageResponse(bool sucess)
        {
            Sucess = sucess;
        }
    }
}
