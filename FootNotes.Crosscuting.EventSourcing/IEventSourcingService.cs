using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KurrentDB.Client; 

namespace FootNotes.Crosscuting.EventSourcing
{
    public interface IEventSourcingService
    {
        KurrentDBClient GetClient();
    }
}
