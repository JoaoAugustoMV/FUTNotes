using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Application;
using FootNotes.IAM.Application.Commands;
using FootNotes.IAM.Domain;

namespace FootNotes.IAM.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserStatusResponse>> RegisterUserAsync(UserRegisterCommand userRegisterCommand);
        Task<Result<UserStatusResponse>> UpdateUserAsync(UserUpdateCommand userUpdateCommand);
    }
}
