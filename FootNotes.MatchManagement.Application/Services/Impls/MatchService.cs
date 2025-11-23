using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Application;
using FootNotes.Core.Data.Communication;
using FootNotes.Core.Messages;
using FootNotes.MatchManagement.Application.Commands;
using FootNotes.MatchManagement.Application.Commands.MatchCommands;
using FootNotes.MatchManagement.Application.Events.TeamEvents;
using FootNotes.MatchManagement.Application.Providers;
using FootNotes.MatchManagement.Application.Requests;
using FootNotes.MatchManagement.Domain.MatchModels;
using FootNotes.MatchManagement.Domain.Repository;
using FootNotes.MatchManagement.Domain.TeamModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FootNotes.MatchManagement.Application.Services.Impls
{
    public class MatchService(ITeamRepository teamRepository,
        IMediatorHandler mediatorHandler,
        IMatchProvider matchProvider,
        IMatchRepository matchRepository,
        ILogger<MatchService> logger) : IMatchService
    {        

        public async Task<Result<Guid>> CreateMatchManually(CreateMatchManuallyRequest request)
        {
            if (!request.IsValid(out string error))
            {
                return Result<Guid>.Failure(error);
            }

            Result<Guid> homeTeamResponse = await CreateTeamIfNotExists(request.HomeTeamName);
            if (!homeTeamResponse.Successed)
            {
                return Result<Guid>.Failure(homeTeamResponse.Error ?? string.Empty);
            }

            Result<Guid> awayTeamResponse = await CreateTeamIfNotExists(request.AwayTeamName);
            if (!awayTeamResponse.Successed)
            {
                return Result<Guid>.Failure(awayTeamResponse.Error ?? string.Empty);
            }

            CreateMatchManuallyCommand command = new(
                request.UserId,
                homeTeamResponse.Value,
                awayTeamResponse.Value,
                request.MatchDate,
                request.Status,
                request.DecisionType,
                request.HomeScore,
                request.AwayScore,
                request.HomePenaltyScore,
                request.AwayPenaltyScore
            );

            CommandResponse response = await mediatorHandler.SendCommand(command);

            return response.Sucess ? Result<Guid>.Success(response.AggregateId) : Result<Guid>.Failure(response.Message!);
        }

        public async Task<Result<bool>> ChangeMatchStatus(UpdateStatusMatchRequest request)
        {
            if (!request.IsValid(out string error))
            {
                return Result<bool>.Failure(error);
            }
            
            UpdateMatchStatusCommand command = new(request.MatchId,request.NewStatus);
            
            CommandResponse response = await mediatorHandler.SendCommand(command);

            return response.Sucess ? Result<bool>.Success(true) : Result<bool>.Failure(response.Message!);
        }


        public async Task<Result<bool>> UpdateMatchScore(UpdateScoreMatchRequest request)
        {
            if (!request.IsValid(out string error))
            {
                return Result<bool>.Failure(error);
            }

            UpdateScoreMatchCommand command = new(request.MatchId, request.TeamId);

            CommandResponse response = await mediatorHandler.SendCommand(command);

            return response.Sucess ? Result<bool>.Success(true) : Result<bool>.Failure(response.Message!);
        }


        private async Task<Result<Guid>> CreateTeamIfNotExists(string teamName)
        {
            Guid teamId = await teamRepository.GetIdByName(teamName);
            if (teamId == Guid.Empty)
            {                
                CommandResponse response = await mediatorHandler
                    .SendCommand(new CreateTeamManuallyCommand(teamName));

                if (!response.Sucess)
                {
                    return Result<Guid>.Failure(response.Message!);
                }

                return Result<Guid>.Success(response.AggregateId);
            }

            return Result<Guid>.Success(teamId);

        }

        public async Task ProcessUpcommingMatch()
        {
            IEnumerable<UpcomingMatchInfo> upcomingMatchsProvider = await matchProvider.GetUpcomingMatchs();

            if (upcomingMatchsProvider == null || !upcomingMatchsProvider.Any())
            {
                logger.LogInformation("No upcoming matchs found from provider.");
                return;
            }

            IEnumerable<string>? upcomingMatchsCodes = upcomingMatchsProvider
                .Select(m => Match.GenerateCode(m.HomeTeamInfo.Code, m.AwayTeamInfo.Code, m.MatchDate));

            HashSet<string> registeredMatchs = [.. await matchRepository.GetAllByCodes(upcomingMatchsCodes).Select(m => m.Code).ToListAsync()];

            List<UpcomingMatchInfo> matchesToInsert = [];
            foreach (UpcomingMatchInfo item in upcomingMatchsProvider)
            {
                if(!registeredMatchs.Contains(Match.GenerateCode(item.HomeTeamInfo.Code, item.AwayTeamInfo.Code, item.MatchDate)))
                {
                    matchesToInsert.Add(item);
                }
            }

            if(matchesToInsert.Count == 0)
            {
                logger.LogInformation("No new upcoming matchs to insert.");
                return;
            }

            await mediatorHandler.SendCommand(new InsertUpcomingMatchsCommand()
            {
                MatchInfos = matchesToInsert
            });

        }
    }
}
