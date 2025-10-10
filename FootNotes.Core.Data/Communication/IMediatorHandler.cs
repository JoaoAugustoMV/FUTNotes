using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;

namespace FootNotes.Core.Data.Communication
{
    public interface IMediatorHandler
    {        
        Task PublishEvent<T>(T evento) where T : Event;
        Task<MessageResponse> SendCommand<T>(T comando) where T : Command;
        //Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
        //Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent;
    }
}
