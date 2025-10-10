using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.IAM.Application.Queries.Interfaces;
using FootNotes.IAM.Application.Queries.ViewModels;
using FootNotes.IAM.Domain;
using FootNotes.IAM.Domain.Repository;

namespace FootNotes.IAM.Application.Queries
{
    public class UserQueries(IUserRepository userRepository) : IUserQueries
    {
        public async Task<UserViewModel?> GetUserViewModelByEmail(string email)
        {
            User? user = await userRepository.GetByEmailAsync(email);
            if (user == null) return null;

            return new UserViewModel
            {
                Username = user.Username,
                Password = user.PasswordHash
            };
            
        }
    }
}
