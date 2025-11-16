using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.MatchManagement.Application.Providers;
using FootNotes.MatchManagement.Application.Services.Impls;
using Moq;
using Moq.AutoMock;

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
            int expected = 5;

            // Act
            int actual = 2 + 3;
            await matchService.ProcessUpcommingMatch();

            // Assert
            _mocker.GetMock<IMatchProvider>().Verify(
                    m => m.GetUpcomingMatchs(),
                    Times.Once
                );

            //_mocker.

            //Assert.Equal(expected, actual);
        }
    }
}
