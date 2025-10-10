using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Data;

namespace FootNotes.IAM.Domain.Repository
{
    public interface IUserRepository: IRepositoryBase<User>
    {
        public Task<User?> GetByUsernameAsync(string username);
        public Task<User?> GetByEmailAsync(string email);
        public Task<bool> IsEmailInUseAsync(string email);
        public Task<bool> IsUsernameInUseAsync(string username);
        public Task<bool> IsUsernameInUseAsync(string username, string email);
    }
}
