using Xunit;
using static FootballTeamsRanking.Team;

namespace FootballTeamsRanking.Test
{
    public class TeamFacts
    {
        [Fact]
        public void NameIsNotEmpty()
        {
            Team team1 = new Team("", 16);
            Team team2 = new Team("Real Madrid", 16);
            Assert.Equal("", team1.Name);
            Assert.Equal("Real Madrid", team2.Name);
        }
    }
}
