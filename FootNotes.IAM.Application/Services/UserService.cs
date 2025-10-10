using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Application;
using FootNotes.Core.Data.Communication;
using FootNotes.Core.Messages;
using FootNotes.IAM.Application.Commands;
using FootNotes.IAM.Application.Interfaces;
using FootNotes.IAM.Domain;
using FootNotes.IAM.Domain.Repository;

namespace FootNotes.IAM.Application.Services
{
    public class UserService(IUserRepository userRepository, IMediatorHandler mediatorHandler) : IUserService
    {        
        public async Task<Result<UserStatusResponse>> RegisterUserAsync(UserRegisterCommand userRegisterCommand)
        {
            if (!userRegisterCommand.IsValid(out string msg))
            {
                return Result<UserStatusResponse>.Failure(msg, UserStatusResponse.Invalid);
            }

            if (await userRepository.IsEmailInUseAsync(userRegisterCommand.Email))
            {
                return Result<UserStatusResponse>.Failure("Email in use", UserStatusResponse.EmailAlreadyInUse);
            }

            if (await userRepository.IsUsernameInUseAsync(userRegisterCommand.Username))
            {
                return Result<UserStatusResponse>.Failure("Email in use", UserStatusResponse.UsernameAlreadyInUse);
            }

            MessageResponse response = await mediatorHandler.SendCommand(userRegisterCommand);

            if (response.Sucess)
            {
                return Result<UserStatusResponse>.Success(UserStatusResponse.Created);
            }

            return Result<UserStatusResponse>.Failure(response.Message!);

        }

        public async Task<Result<UserStatusResponse>> UpdateUserAsync(UserUpdateCommand userUpdateCommand)
        {
            if (!userUpdateCommand.IsValid(out string msg))
            {
                return Result<UserStatusResponse>.Failure(msg, UserStatusResponse.Invalid);
            }
            
            if (await userRepository.IsUsernameInUseAsync(userUpdateCommand.Username, userUpdateCommand.Email))
            {
                return Result<UserStatusResponse>.Failure("Username in use", UserStatusResponse.UsernameAlreadyInUse);
            }

            MessageResponse response = await mediatorHandler.SendCommand(userUpdateCommand);

            if (response.Sucess)
            {
                return Result<UserStatusResponse>.Success(UserStatusResponse.Created);
            }

            return Result<UserStatusResponse>.Failure(response.Message!);

        }

    }
}
