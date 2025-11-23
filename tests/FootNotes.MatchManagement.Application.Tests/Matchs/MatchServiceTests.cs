using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Core.Data.Communication;
using FootNotes.MatchManagement.Application.Commands.MatchCommands;
using FootNotes.MatchManagement.Application.DTOs;
using FootNotes.MatchManagement.Application.Providers;
using FootNotes.MatchManagement.Application.Services.Impls;
using FootNotes.MatchManagement.Domain.Repository;
using Moq;
using Moq.AutoMock;
using MockQueryable;

using Match = FootNotes.MatchManagement.Domain.MatchModels.Match;

namespace FootNotes.MatchManagement.Application.Tests.Matchs
{
    public class MatchServiceTests
    {
        private readonly AutoMocker _mocker;
        public MatchServiceTests()
        {
            _mocker = new AutoMocker();
        }

        [Fact]
        public async Task ProcessUpcommingMatch_WithNoNewMatch()
        {
            // Arrange
            MatchService matchService = _mocker.CreateInstance<MatchService>();

            // Act
            await matchService.ProcessUpcommingMatch();

            // Assert
            _mocker.GetMock<IMatchProvider>().Verify(
                    m => m.GetUpcomingMatchs(),
                    Times.Once
                );
        }

        [Fact]
        public async Task ProcessUpcommingMatch_WithAllUpcomingMatchsUnregistered_InsertAll()
        {
            // Arrange
            MatchService matchService = _mocker.CreateInstance<MatchService>();

            IQueryable<Match> existingMatchs = new List<Match>().BuildMock();
            List<UpcomingMatchInfo> upcomingMatchs = [
                    new UpcomingMatchInfo(
                        Guid.NewGuid(),
                        DateTime.UtcNow.AddDays(1),
                        new TeamInfoDTO("Team A", "team-a"),
                        new TeamInfoDTO("Team B", "team-b")
                    ),
                    new UpcomingMatchInfo(
                        Guid.NewGuid(),
                        DateTime.UtcNow.AddDays(2),
                        new TeamInfoDTO("Team C", "team-c"),
                        new TeamInfoDTO("Team D", "team-c")
                    )
                ];

            _mocker.GetMock<IMatchProvider>()
                .Setup(m => m.GetUpcomingMatchs())
                .ReturnsAsync(upcomingMatchs);

            _mocker.GetMock<IMatchRepository>()
                .Setup(m => m.GetAllByCodes(It.IsAny<IEnumerable<string>>()))
                .Returns(existingMatchs);

            // Act
            await matchService.ProcessUpcommingMatch();

            // Assert
            _mocker.GetMock<IMatchProvider>().Verify(
                    m => m.GetUpcomingMatchs(),
                    Times.Once
                );

            _mocker.GetMock<IMediatorHandler>().Verify(
                    m => m.SendCommand(It.Is<InsertUpcomingMatchsCommand>(m => m.MatchInfos.Count() == upcomingMatchs.Count)),
                    Times.Once
                );
            
        }

        [Fact]
        public async Task ProcessUpcommingMatch_WithAllUpcomingMatchsRegisteredMatchs_InsertNone()
        {
            // Arrange
            MatchService matchService = _mocker.CreateInstance<MatchService>();
            List<UpcomingMatchInfo> upcomingMatchsProvider = [
                    new UpcomingMatchInfo(
                        Guid.NewGuid(),
                        DateTime.UtcNow.AddDays(1),
                        new TeamInfoDTO("Team A", "team-a"),
                        new TeamInfoDTO("Team B", "team-b")
                    ),
                    new UpcomingMatchInfo(
                        Guid.NewGuid(),
                        DateTime.UtcNow.AddDays(2),
                        new TeamInfoDTO("Team C", "team-c"),
                        new TeamInfoDTO("Team D", "team-c")
                    )
                ];

            IQueryable<Match> existingMatchs = upcomingMatchsProvider
                .Select(
                    m => Match.CreateUpcoming(
                        Match.GenerateCode(m.HomeTeamInfo.Code, m.AwayTeamInfo.Code, m.MatchDate), 
                        Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddDays(1))).ToList().BuildMock();

            _mocker.GetMock<IMatchProvider>()
                .Setup(m => m.GetUpcomingMatchs())
                .ReturnsAsync(upcomingMatchsProvider);

            _mocker.GetMock<IMatchRepository>()
                .Setup(m => m.GetAllByCodes(It.IsAny<IEnumerable<string>>()))
                .Returns(existingMatchs);

            // Act
            await matchService.ProcessUpcommingMatch();

            // Assert
            _mocker.GetMock<IMatchProvider>().Verify(
                    m => m.GetUpcomingMatchs(),
                    Times.Once
                );

            _mocker.GetMock<IMediatorHandler>().Verify(
                    m => m.SendCommand(It.IsAny<InsertUpcomingMatchsCommand>()),
                    Times.Never
                );
        }

        [Fact]
        public async Task ProcessUpcommingMatch_WithUpcomingMatchsRegisteredAndUnregisteredMatchs_InsertOnlyUnregistered()
        {
            // Arrange
            MatchService matchService = _mocker.CreateInstance<MatchService>();
            List<UpcomingMatchInfo> upcomingMatchsProvider = [
                    new UpcomingMatchInfo(
                        Guid.NewGuid(),
                        DateTime.UtcNow.AddDays(1),
                        new TeamInfoDTO("Team A", "team-a"),
                        new TeamInfoDTO("Team B", "team-b")
                    ),
                    new UpcomingMatchInfo(
                        Guid.NewGuid(),
                        DateTime.UtcNow.AddDays(2),
                        new TeamInfoDTO("Team C", "team-c"),
                        new TeamInfoDTO("Team D", "team-c")
                    ),
                    new UpcomingMatchInfo(
                        Guid.NewGuid(),
                        DateTime.UtcNow.AddDays(3),
                        new TeamInfoDTO("Team E", "team-e"),
                        new TeamInfoDTO("Team F", "team-f")
                    ),
                    new UpcomingMatchInfo(
                        Guid.NewGuid(),
                        DateTime.UtcNow.AddDays(4),
                        new TeamInfoDTO("Team G", "team-g"),
                        new TeamInfoDTO("Team H", "team-h")                        
                    ),
                    new UpcomingMatchInfo(
                        Guid.NewGuid(),
                        DateTime.UtcNow.AddDays(5),
                        new TeamInfoDTO("Team I", "team-i"),
                        new TeamInfoDTO("Team J", "team-j")
                    )
                ];

            IQueryable<Match> existingMatchs = upcomingMatchsProvider.GetRange(0, 2)
                .Select(
                    m => Match.CreateUpcoming(
                        Match.GenerateCode(m.HomeTeamInfo.Code, m.AwayTeamInfo.Code, m.MatchDate),
                        Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddDays(1))).ToList().BuildMock();
            

            _mocker.GetMock<IMatchProvider>()
                .Setup(m => m.GetUpcomingMatchs())
                .ReturnsAsync(upcomingMatchsProvider);

            _mocker.GetMock<IMatchRepository>()
                .Setup(m => m.GetAllByCodes(It.IsAny<IEnumerable<string>>()))
                .Returns(existingMatchs);

            // Act
            await matchService.ProcessUpcommingMatch();

            // Assert
            _mocker.GetMock<IMatchProvider>().Verify(
                    m => m.GetUpcomingMatchs(),
                    Times.Once
                );


            _mocker.GetMock<IMediatorHandler>().Verify(
                    mh => mh.SendCommand(
                        It.Is<InsertUpcomingMatchsCommand>(c => c.MatchInfos.Count() == upcomingMatchsProvider.Count - existingMatchs.Count())),
                    Times.Once
                );

        }

    }
}
