using FootNotes.Core.Data;
using FootNotes.Core.Data.Communication;
using FootNotes.IAM.Application.Queries.ViewModels;
using FootNotes.IAM.Domain;
using FootNotes.IAM.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace FootNotes.IAM.Data
{
    public class UserRepository(IAMContext dbContext, IMediatorHandler mediator) : RepositoryBase<User, IAMContext>(dbContext, mediator), IUserRepository
    {
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);            
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public Task<bool> IsEmailInUseAsync(string email)
        {
            return dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public Task<bool> IsUsernameInUseAsync(string username)
        {
            return dbContext.Users.AnyAsync(u => u.Username == username);
        }

        public Task<bool> IsUsernameInUseAsync(string username, string email)
        {
            return dbContext.Users.AnyAsync(u => u.Username == username && u.Email != email);
        }

        public async Task<UserViewModel?> GetViewModelByEmail(string email)
        {
            return await dbContext.Users
                .Where(p => p.Email == email)
                .Select(u => new UserViewModel()
                {
                    Username = u.Username,
                    Password = u.PasswordHash,
                })
                .FirstOrDefaultAsync();
            
        }
    }
}
