using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FootNotes.Core.Messages
{
    public abstract class Query<T>: IRequest<T>
    {        
    }
}
