using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.IAM.Domain
{
    public enum UserStatusResponse
    {
        Created,
        Invalid,
        EmailNotValid,
        EmailAlreadyInUse,
        UsernameAlreadyInUse,
        WeakPassword,
    }
}
