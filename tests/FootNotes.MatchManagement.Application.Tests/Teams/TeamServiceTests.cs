using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.MatchManagement.Application.DTOs;
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
            TeamInfoDTO homeTeamInfo = new("Liverpool", "liverpool");
            TeamInfoDTO awayTeamInfo = new("Manchester City", "manchester-city");
            IQueryable<Team> teams = new List<Team>
                {
                    Team.CreateNotManually(homeTeamInfo.Name, homeTeamInfo.Code),
                    Team.CreateNotManually(awayTeamInfo.Name, awayTeamInfo.Code)
                }.BuildMock();

            TeamService teamService = _mocker.CreateInstance<TeamService>();
            _mocker.GetMock<ITeamRepository>()
                .Setup(r => r.GetByTeamsCode(It.IsAny<IEnumerable<string>>()))
                .Returns(teams);

            // Act
            Dictionary<string, Guid> dict = await teamService.GetIdOrCreateTeamsAsync([homeTeamInfo, awayTeamInfo]);

            // Assert
            _mocker.GetMock<ITeamRepository>()
                .Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<Team>>()), Times.Never);

            Assert.NotNull(dict);
            Assert.Equal(2, dict.Count);

            Guid homeTeamId = dict.GetValueOrDefault(homeTeamInfo.Code);
            Guid awayTeamId = dict.GetValueOrDefault(awayTeamInfo.Code);

            Assert.NotEqual(Guid.Empty, homeTeamId);
            Assert.NotEqual(Guid.Empty, awayTeamId);
        }

        [Fact]
        public async Task GetIdOrCreateTeamsAsync_WithTwoUnregisteredTeams_ShouldInsertBothAnyAndReturnIds()
        {
            // Arrange            
            TeamInfoDTO homeTeamInfo = new("Liverpool", "liverpool");
            TeamInfoDTO awayTeamInfo = new("Manchester City", "manchester-city");
            IQueryable<Team> teams = new List<Team>
                {                    
                }.BuildMock();

            TeamService teamService = _mocker.CreateInstance<TeamService>();
            _mocker.GetMock<ITeamRepository>()
                .Setup(r => r.GetByTeamsCode(It.IsAny<IEnumerable<string>>()))
                .Returns(teams);

            // Act
            Dictionary<string, Guid> dict = await teamService.GetIdOrCreateTeamsAsync([homeTeamInfo, awayTeamInfo]);

            // Assert
            _mocker.GetMock<ITeamRepository>()
                .Verify(r => 
                    r.AddRangeAsync(It.Is<IEnumerable<Team>>(ts => ts.All(t => t.Events.Count != 0 && t.HasCreatedManually == false))),
                    Times.Once);

            Assert.NotNull(dict);
            Assert.Equal(2, dict.Count);

            Guid homeTeamId = dict.GetValueOrDefault(homeTeamInfo.Code);
            Guid awayTeamId = dict.GetValueOrDefault(awayTeamInfo.Code);

            Assert.NotEqual(Guid.Empty, homeTeamId);
            Assert.NotEqual(Guid.Empty, awayTeamId);
        }

        [Fact]
        public async Task GetIdOrCreateTeamsAsync_WithOneUnregisteredTeams_ShouldInsertOneAnyAndReturnIds()
        {
            // Arrange            
            TeamInfoDTO homeTeamInfo = new("Liverpool", "liverpool");
            TeamInfoDTO awayTeamInfo = new("Manchester City", "manchester-city");
            IQueryable<Team> teams = new List<Team>
            {
                Team.CreateNotManually(homeTeamInfo.Name, homeTeamInfo.Code)
            }.BuildMock();

            TeamService teamService = _mocker.CreateInstance<TeamService>();
            _mocker.GetMock<ITeamRepository>()
                .Setup(r => r.GetByTeamsCode(It.IsAny<IEnumerable<string>>()))
                .Returns(teams);

            // Act
            Dictionary<string, Guid> dict = await teamService.GetIdOrCreateTeamsAsync([homeTeamInfo, awayTeamInfo]);

            // Assert
            _mocker.GetMock<ITeamRepository>()
                .Verify(r =>
                    r.AddRangeAsync(It.Is<IEnumerable<Team>>(ts => ts.All(t => t.Events.Count != 0 && t.HasCreatedManually == false) && ts.Count() == 1)),
                    Times.Once);

            Assert.NotNull(dict);
            Assert.Equal(2, dict.Count);

            Guid homeTeamId = dict.GetValueOrDefault(homeTeamInfo.Code);
            Guid awayTeamId = dict.GetValueOrDefault(awayTeamInfo.Code);

            Assert.NotEqual(Guid.Empty, homeTeamId);
            Assert.NotEqual(Guid.Empty, awayTeamId);
        }
    }
}
