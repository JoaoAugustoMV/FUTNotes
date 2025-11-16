using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.MatchManagement.Application.Services.Impls;
using FootNotes.MatchManagement.Domain.Repository;
using FootNotes.MatchManagement.Domain.TeamModels;
using MockQueryable;
using Moq;
using Moq.AutoMock;

namespace FootNotes.MatchManagement.Application.Tests.Teams
{
    // TODO: Implement TeamServiceTests
    public class TeamServiceTests
    {
        private readonly AutoMocker _mocker;
        public TeamServiceTests()
        {
            _mocker = new AutoMocker();
        }

        [Fact]
        public async Task GetIdOrCreateTeamsAsync_WithRegisteredTeams_ShouldNotInsertAnyAndReturnIds()
        {
            // Arrange
            string homeTeamName = "TEAM_A";
            string awayTeamName = "TEAM_B";
            IQueryable<Team> teams = new List<Team>
                {
                    Team.CreateManually(homeTeamName),
                    Team.CreateManually(awayTeamName)
                }.BuildMock();

            TeamService teamService = _mocker.CreateInstance<TeamService>();
            _mocker.GetMock<ITeamRepository>()
                .Setup(r => r.GetByTeamsName(It.IsAny<string[]>()))
                .Returns(teams);

            // Act
            Dictionary<string, Guid> dict = await teamService.GetIdOrCreateTeamsAsync([homeTeamName, awayTeamName]);

            // Assert
            _mocker.GetMock<ITeamRepository>()
                .Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<Team>>()), Times.Never);

            Assert.NotNull(dict);
            Assert.Equal(2, dict.Count);

            Guid homeTeamId = dict.GetValueOrDefault(homeTeamName);
            Guid awayTeamId = dict.GetValueOrDefault(awayTeamName);

            Assert.NotEqual(Guid.Empty, homeTeamId);
            Assert.NotEqual(Guid.Empty, awayTeamId);
        }

        [Fact]
        public async Task GetIdOrCreateTeamsAsync_WithTwoUnregisteredTeams_ShouldInsertBothAnyAndReturnIds()
        {
            // Arrange            
            string homeTeamName = "TEAM_A";
            string awayTeamName = "TEAM_B";
            IQueryable<Team> teams = new List<Team>
                {                    
                }.BuildMock();

            TeamService teamService = _mocker.CreateInstance<TeamService>();
            _mocker.GetMock<ITeamRepository>()
                .Setup(r => r.GetByTeamsName(It.IsAny<string[]>()))
                .Returns(teams);

            // Act
            Dictionary<string, Guid> dict = await teamService.GetIdOrCreateTeamsAsync([homeTeamName, awayTeamName]);

            // Assert
            _mocker.GetMock<ITeamRepository>()
                .Verify(r => 
                    r.AddRangeAsync(It.Is<IEnumerable<Team>>(ts => ts.All(t => t.Events.Count != 0 && t.HasCreatedManually == false))),
                    Times.Once);

            Assert.NotNull(dict);
            Assert.Equal(2, dict.Count);

            Guid homeTeamId = dict.GetValueOrDefault(homeTeamName);
            Guid awayTeamId = dict.GetValueOrDefault(awayTeamName);

            Assert.NotEqual(Guid.Empty, homeTeamId);
            Assert.NotEqual(Guid.Empty, awayTeamId);
        }

        [Fact]
        public async Task GetIdOrCreateTeamsAsync_WithOneUnregisteredTeams_ShouldInsertOneAnyAndReturnIds()
        {
            // Arrange            
            string homeTeamName = "TEAM_A";
            string awayTeamName = "TEAM_B";
            IQueryable<Team> teams = new List<Team>
            {
                Team.CreateNotManually(homeTeamName)
            }.BuildMock();

            TeamService teamService = _mocker.CreateInstance<TeamService>();
            _mocker.GetMock<ITeamRepository>()
                .Setup(r => r.GetByTeamsName(It.IsAny<string[]>()))
                .Returns(teams);

            // Act
            Dictionary<string, Guid> dict = await teamService.GetIdOrCreateTeamsAsync([homeTeamName, awayTeamName]);

            // Assert
            _mocker.GetMock<ITeamRepository>()
                .Verify(r =>
                    r.AddRangeAsync(It.Is<IEnumerable<Team>>(ts => ts.All(t => t.Events.Count != 0 && t.HasCreatedManually == false) && ts.Count() == 1)),
                    Times.Once);

            Assert.NotNull(dict);
            Assert.Equal(2, dict.Count);

            Guid homeTeamId = dict.GetValueOrDefault(homeTeamName);
            Guid awayTeamId = dict.GetValueOrDefault(awayTeamName);

            Assert.NotEqual(Guid.Empty, homeTeamId);
            Assert.NotEqual(Guid.Empty, awayTeamId);
        }
    }
}
