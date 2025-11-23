using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Messages;
using FootNotes.MatchManagement.Application.Commands;
using FootNotes.MatchManagement.Application.Commands.MatchCommands;
using FootNotes.MatchManagement.Application.Events.MatchEvents;
using FootNotes.MatchManagement.Application.Providers;
using FootNotes.MatchManagement.Application.Services;
using FootNotes.MatchManagement.Domain.MatchModels;
using FootNotes.MatchManagement.Domain.Repository;
using FootNotes.MatchManagement.Domain.TeamModels;
using MediatR;

namespace FootNotes.MatchManagement.Application.CommandHandlers
{
    public class MatchCommandHandler(IMatchRepository matchRepository, ITeamRepository teamRepository, ITeamService teamService) : 
		IRequestHandler<CreateMatchManuallyCommand, CommandResponse>,
        IRequestHandler<UpdateMatchStatusCommand, CommandResponse>,
        IRequestHandler<UpdateScoreMatchCommand, CommandResponse>,
        IRequestHandler<InsertUpcomingMatchsCommand, CommandResponse>
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

        public async Task<CommandResponse> Handle(UpdateMatchStatusCommand command, CancellationToken cancellationToken)
        {
			if (!command.IsValid(out string error))
			{
				return new CommandResponse(Guid.Empty, false, error);
            }

			Match? match = await matchRepository.GetByIdAsync(command.MatchId);

			if (match == null)
			{				
				return new CommandResponse(Guid.Empty, false, "Match not found.");
			}


			match.UpdateStatus(command.NewStatus);
			match.AddEvent(new UpdateMatchStatusEvent(
				command.MatchId,
				command.NewStatus
			));
			await matchRepository.UpdateAsync(match);
			return new CommandResponse(match.Id, true);
        }

        public async Task<CommandResponse> Handle(UpdateScoreMatchCommand command, CancellationToken cancellationToken)
        {
            if(!command.IsValid(out string error))
			{
				return new CommandResponse(Guid.Empty, false, error);
            }

			Match? match = await matchRepository.GetByIdAsync(command.MatchId);
			if(match == null)
			{
				return new CommandResponse(Guid.Empty, false, "Match not found.");
            }

			bool validTeam = false;
            if (match.HomeTeamId == command.TeamId)
			{
				validTeam = true;
                match.UpdateHomeScore();
            }else if (match.AwayTeamId == command.TeamId)
			{
				validTeam = true;
				match.UpdateAwayScore();
            }

			if (!validTeam)
			{
                return new CommandResponse(Guid.Empty, false, "The team is not part of the match.");
			}

			match.AddEvent(new UpdateScoreMatchEvent(match.Id, command.TeamId, match.HomeScore ?? 0, match.AwayScore ?? 0));

			await matchRepository.UpdateAsync(match);

			return new CommandResponse(match.Id, true);
        }

        public async Task<CommandResponse> Handle(InsertUpcomingMatchsCommand request, CancellationToken cancellationToken)
        {
			if(!request.IsValid(out string error))
			{
				return new CommandResponse(Guid.Empty, false, error);
            }
			
            Dictionary<string, Guid> teamIdsByName = await teamService.GetIdOrCreateTeamsAsync(
                request.MatchInfos.Select(m => m.HomeTeamInfo).Concat(
                request.MatchInfos.Select(m => m.AwayTeamInfo))
                );

			List<Match> newMatches = [];
            foreach (UpcomingMatchInfo item in request.MatchInfos)
            {
				Match match = Match.CreateUpcoming(
					Match.GenerateCode(item.HomeTeamInfo.Code, item.AwayTeamInfo.Code, item.MatchDate),
					teamIdsByName[item.HomeTeamInfo.Code],
					teamIdsByName[item.AwayTeamInfo.Code],
					item.CompetitionId, item.MatchDate);
			
				match.AddEvent(new InsertUpcomingMatchEvent(match.Id)
				{
					HomeTeamId = match.HomeTeamId,
					AwayTeamId = match.AwayTeamId,
					CompetitionId = match.CompetitionId!.Value,
					MatchDate = match.MatchDate!.Value,
				});
                
            }


            await matchRepository.AddRangeAsync(newMatches);

            return new CommandResponse(Guid.NewGuid(), true);
        }
    }
}
