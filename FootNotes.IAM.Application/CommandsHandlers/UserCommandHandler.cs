using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.Crooscutting.Utils;
using FootNotes.IAM.Application.Commands;
using FootNotes.IAM.Application.Events;
using FootNotes.IAM.Domain;
using FootNotes.IAM.Domain.Repository;
using MediatR;

namespace FootNotes.IAM.Application.CommandsHandlers
{
    public class UserCommandHandler(IUserRepository userRepository) : 
        IRequestHandler<UserRegisterCommand, CommandResponse>,
        IRequestHandler<UserUpdateCommand, CommandResponse>
    {
        public async Task<CommandResponse> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            User user;
            try
            {
                string passwordHash = PasswordHelper.HashPassword(request.Password);
                user = new(request.Username, request.Email, passwordHash, request.UserType);
                user.AddEvent(new UserModifiedEvent(                
                    user.Id,
                    user.Username,
                    user.Email,
                    user.CreatedAt,                    
                    user.UserType
                ));
                    
                await userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
              return new CommandResponse(Guid.Empty, false, ex.Message);
            }
            
            return new CommandResponse(user.Id, true);
        }

        public async Task<CommandResponse> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            User? user = null;
            try
            {
                user = await userRepository.GetByEmailAsync(request.Email);

                if(user == null)
                    return new CommandResponse(Guid.Empty, false, "User not found");

                string passwordHash = PasswordHelper.HashPassword(request.Password);

                user.Username = request.Username;
                user.PasswordHash = passwordHash;
                user.UserType = request.UserType;

                user.AddEvent(new UserModifiedEvent(
                     user.Id,
                     user.Username,
                     user.Email,
                     user.CreatedAt,
                     user.UserType
                ));

                await userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return new CommandResponse(Guid.Empty, false, ex.Message);
            }

            return new CommandResponse(user.Id, true);
        }
    }
}
