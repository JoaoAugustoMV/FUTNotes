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
        public async Task<CommandResponse> Handle(CreateMatchManuallyCommand command, CancellationToken cancellationToken)
        {
			try
			{
				if(!command.IsValid(out string error))
				{
					return new CommandResponse(Guid.Empty, false, error);
                }

				Match match = Match.CreateManually(
					command.HomeTeamId,
					command.AwayTeamId,
					command.MatchDate,
					command.Status,
					command.DecisionType,
					command.HomeScore,
					command.AwayScore,
					command.HomePenaltyScore,
					command.AwayPenaltyScore
                );

				match.AddEvent(new CreateMatchManuallyEvent(
					command.UserId,
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
