using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Application;
using FootNotes.MatchManagement.Application.Commands.MatchCommands;
using FootNotes.MatchManagement.Application.Requests;

namespace FootNotes.MatchManagement.Application.Services
{
    public interface IMatchService
    {
        Task<Result<bool>> CreateMatchManually(CreateMatchManuallyRequest command);
    }
}
