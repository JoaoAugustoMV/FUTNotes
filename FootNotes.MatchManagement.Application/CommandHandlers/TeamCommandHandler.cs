using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.MatchManagement.Application.Commands;
using FootNotes.MatchManagement.Application.Events.TeamEvents;
using FootNotes.MatchManagement.Domain.Repository;
using FootNotes.MatchManagement.Domain.TeamModels;
using MediatR;

namespace FootNotes.MatchManagement.Application.CommandHandlers
{
    public class TeamCommandHandler(ITeamRepository teamRepository) : IRequestHandler<CreateTeamManuallyCommand, CommandResponse>
    {
        public async Task<CommandResponse> Handle(CreateTeamManuallyCommand request, CancellationToken cancellationToken)
        {
            Team? team;
            try
            {
                if(!request.IsValid(out string error))
                {
                    return new CommandResponse(Guid.Empty, false, error);
                }

                team = Team.CreateManually(request.TeamName);
            
                team.AddEvent(new CreateTeamManuallyEvent(team.Id, request.TeamName));
                await teamRepository.AddAsync(team);
            }
            catch (Exception ex)
            {
                return new CommandResponse(Guid.Empty, false, ex.Message);
            }
            
            return new CommandResponse(team.Id, true, "Team created successfully");
        }
    }
}
