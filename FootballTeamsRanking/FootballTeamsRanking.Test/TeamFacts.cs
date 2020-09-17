using Xunit;

namespace FootballTeamsRanking.Test
{
    public class TeamFacts
    {
        [Fact]
        public void ComparePointsMethodShouldCompareTeamsPointsCorrectly()
        {
            Team team1 = new Team("Spain", 26);
            Team team2 = new Team("Czech Republic", 18);
            Team team3 = new Team("Serbia", 30);
            Team team4 = new Team("Hungary", 18);
            Team team5 = new Team("Geramny", 0);
            Team team6 = new Team("Portugal", 0);

            Assert.Equal(1, team1.ComparePoints(team2));
            Assert.Equal(-1, team2.ComparePoints(team3));
            Assert.Equal(0, team4.ComparePoints(team2));
            Assert.Equal(0, team5.ComparePoints(team6));
        }

        [Fact]
        public void CompareNamesMethodShouldCompareTeamsNamesCorrectly()
        {
            Team team1 = new Team("Spain", 26);
            Team team2 = new Team("Czech Republic", 18);
            Team team3 = new Team("Serbia", 30);
            Team team4 = new Team("Hungary", 18);
            Team team5 = new Team("Geramny", 0);
            Team team6 = new Team("Portugal", 0);
            Team team7 = new Team("Portugal", 0);

            Assert.Equal(1, team1.CompareNames(team2));
            Assert.Equal(-1, team2.CompareNames(team3));
            Assert.Equal(1, team4.CompareNames(team2));
            Assert.Equal(-1, team5.CompareNames(team6));
            Assert.Equal(0, team6.CompareNames(team7));
        }
    }
}
