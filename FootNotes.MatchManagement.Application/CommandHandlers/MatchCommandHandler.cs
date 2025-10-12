using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.MatchManagement.Application.Commands;
using FootNotes.MatchManagement.Application.Commands.MatchCommands;
using FootNotes.MatchManagement.Application.Events.MatchEvents;
using FootNotes.MatchManagement.Domain.MatchModels;
using FootNotes.MatchManagement.Domain.Repository;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FootNotes.MatchManagement.Application.CommandHandlers
{
    public class MatchCommandHandler(IMatchRepository matchRepository) : IRequestHandler<CreateMatchManuallyCommand, CommandResponse>
    {
        public async Task<CommandResponse> Handle(CreateMatchManuallyCommand request, CancellationToken cancellationToken)
        {
			try
			{
				if(request.IsValid(out string error))
				{
					return new CommandResponse(Guid.Empty, false, error);
                }

				Match match = Match.CreateManually(
					request.HomeTeamId,
					request.AwayTeamId,
					request.MatchDate,
					request.Status,
					request.DecisionType,
					request.HomeScore,
					request.AwayScore,
					request.HomePenaltyScore,
					request.AwayPenaltyScore
                );

				match.AddEvent(new CreateMatchManuallyEvent(
					request.UserId,
                    match.Id,
					match.HomeTeamId,
					match.AwayTeamId,
					match.MatchDate,
					match.Status,
					match.DecisionType,
					match.HomeScore,
					match.AwayScore,
					match.HomePenaltyScore,
					match.AwayPenaltyScore
				));

				await matchRepository.AddAsync(match);

				return new CommandResponse(match.Id, true);
            }
			catch (Exception ex)
			{
                return new CommandResponse(Guid.Empty, false, ex.Message);
            }
        }
    }
}
