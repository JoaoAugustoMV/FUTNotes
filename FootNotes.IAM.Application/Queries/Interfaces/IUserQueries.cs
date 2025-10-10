using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.IAM.Application.Queries.ViewModels;

namespace FootNotes.IAM.Application.Queries.Interfaces
{
    public interface IUserQueries
    {
        Task<UserViewModel?> GetUserViewModelByEmail(string email);
    }
}
