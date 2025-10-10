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
        IRequestHandler<UserRegisterCommand, MessageResponse>,
        IRequestHandler<UserUpdateCommand, MessageResponse>
    {
        public async Task<MessageResponse> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string passwordHash = PasswordHelper.HashPassword(request.Password);
                User user = new(request.Username, request.Email, passwordHash, request.UserType);
                user.AddEvent(new UserModifiedEvent(user.Id, EventCRUDType.Created)
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    Password = passwordHash,
                    UserType = user.UserType,
                });
                    
                await userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
              return new MessageResponse(false, ex.Message);
            }
            
            return new MessageResponse(true);
        }

        public async Task<MessageResponse> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User? user = await userRepository.GetByEmailAsync(request.Email);

                if(user == null)
                    return new MessageResponse(false, "User not found");

                string passwordHash = PasswordHelper.HashPassword(request.Password);

                user.Username = request.Username;
                user.PasswordHash = passwordHash;
                user.UserType = request.UserType;

                user.AddEvent(new UserModifiedEvent(user.Id, EventCRUDType.Updated)
                {                    
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Password = passwordHash,
                    UserType = user.UserType,
                });

                await userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return new MessageResponse(false, ex.Message);
            }

            return new MessageResponse(true);
        }
    }
}
