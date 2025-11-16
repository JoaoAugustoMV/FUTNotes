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
        public async Task InsertMatchCommand_WithInvalidCommand_ShouldReturnFail()
        {
            // Arrange
            TeamInfoDTO homeTeamInfo = new("Manchester City", "manchester-city");
            TeamInfoDTO awayTeamInfo = new("Manchester City", "manchester-city");
            InsertUpcomingMatchCommand command = new()
            {
                HomeTeamInfo = homeTeamInfo,
                AwayTeamInfo = awayTeamInfo,
                CompetitionId = Guid.NewGuid(),
                MatchDate = DateTime.UtcNow
            };
            MatchCommandHandler handler = _mocker.CreateInstance<MatchCommandHandler>();

            // Act
            CommandResponse result = await handler.Handle(command, CancellationToken.None);

            // Assert
            _mocker.GetMock<IMatchRepository>().Verify(
                    r => r.AddAsync(It.IsAny<Match>()), Times.Never
                );
            Assert.NotNull(result);
            Assert.False(result.Sucess);
            Assert.Equal(Guid.Empty, result.AggregateId);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task InsertMatchCommand_WithValidCommand_ShouldInsertNewMatchAndReturnSucess()
        {
            // Arrange            
            TeamInfoDTO homeTeamInfo = new ("Liverpool", "liverpool");
            TeamInfoDTO awayTeamInfo = new ("Manchester City", "manchester-city");
            InsertUpcomingMatchCommand command = new()
            {
                HomeTeamInfo = homeTeamInfo,
                AwayTeamInfo = awayTeamInfo,
                CompetitionId = Guid.NewGuid(),
                MatchDate = DateTime.UtcNow
            };

            MatchCommandHandler handler = _mocker.CreateInstance<MatchCommandHandler>();

            _mocker.GetMock<ITeamService>()
                .Setup(r => r.GetIdOrCreateTeamsAsync(It.IsAny<TeamInfoDTO[]>()))
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
                    r => r.AddAsync(It.Is<Match>(m => m.Events.Count != 0 && m.Status == MatchStatus.Scheduled)), Times.Once
                );
            
            Assert.NotNull(result);
            Assert.True(result.Sucess);
            Assert.NotEqual(Guid.Empty, result.AggregateId);
            Assert.Null(result.Message);
        }

    }
}
