using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Core.Application
{
    public interface IApiRequest
    {
        bool IsValid(out string msg);
    }
}
