using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Data.Communication;
using FootNotes.Core.Messages;
using FootNotes.MatchManagement.Application.CommandHandlers;
using FootNotes.MatchManagement.Application.Commands.MatchCommands;
using FootNotes.MatchManagement.Application.DTOs;
using FootNotes.MatchManagement.Application.Events.MatchEvents;
using FootNotes.MatchManagement.Application.Providers;
using FootNotes.MatchManagement.Application.Services;
using FootNotes.MatchManagement.Domain.MatchModels;
using FootNotes.MatchManagement.Domain.Repository;
using FootNotes.MatchManagement.Domain.TeamModels;
using Moq;
using Moq.AutoMock;

using Match = FootNotes.MatchManagement.Domain.MatchModels.Match;

namespace FootNotes.MatchManagement.Application.Tests.Matchs
{
    public class MatchCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        public MatchCommandHandlerTests()
        {
            _mocker = new AutoMocker();
        }

        [Fact]
        public async Task InsertUpcomingMatchsCommand_WithInvalidCommand_ShouldReturnFail()
        {
            // Arrange
            TeamInfoDTO homeTeamInfo = new("Manchester City", "manchester-city");
            TeamInfoDTO awayTeamInfo = new("Manchester City", "manchester-city");
            InsertUpcomingMatchsCommand command = new()
            {
                MatchInfos = [
                    new UpcomingMatchInfo(
                         Guid.NewGuid(),
                         DateTime.UtcNow,
                         homeTeamInfo,
                         awayTeamInfo
                        )
                    ]
            };
            MatchCommandHandler handler = _mocker.CreateInstance<MatchCommandHandler>();

            // Act
            CommandResponse result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _mocker.GetMock<IMatchRepository>().Verify(
                    r => r.AddRangeAsync(It.IsAny<IEnumerable<Match>>()), Times.Never
                );
            Assert.NotNull(result);
            Assert.False(result.Sucess);
            Assert.Equal(Guid.Empty, result.AggregateId);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task InsertUpcomingMatchsCommand_WithValidCommand_ShouldInsertNewMatchAndReturnSucess()
        {
            // Arrange            
            TeamInfoDTO homeTeamInfo = new ("Liverpool", "liverpool");
            TeamInfoDTO awayTeamInfo = new ("Manchester City", "manchester-city");
            InsertUpcomingMatchsCommand command = new()
            {
                MatchInfos = [
                    new UpcomingMatchInfo(
                         Guid.NewGuid(),
                         DateTime.UtcNow,
                         homeTeamInfo,
                         awayTeamInfo
                        )
                    ]
            };

            MatchCommandHandler handler = _mocker.CreateInstance<MatchCommandHandler>();

            _mocker.GetMock<ITeamService>()
                .Setup(r => r.GetIdOrCreateTeamsAsync(It.IsAny<IEnumerable<TeamInfoDTO>>()))
                .ReturnsAsync(
                    new Dictionary<string, Guid>
                    {
                        { homeTeamInfo.Code, Guid.NewGuid() },
                        { awayTeamInfo.Code, Guid.NewGuid() }
                    }
                );

            // Act
            CommandResponse result = await handler.Handle(command, CancellationToken.None);


            // Assert
            _mocker.GetMock<IMatchRepository>().Verify(
                    r => r.AddRangeAsync(It.Is<IEnumerable<Match>>(m => m.All(x => x.Events.Count != 0 && x.Status == MatchStatus.Scheduled))), Times.Once
                );
            
            Assert.NotNull(result);
            Assert.True(result.Sucess);
            Assert.NotEqual(Guid.Empty, result.AggregateId);
            Assert.Null(result.Message);
        }

    }
}
