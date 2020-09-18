using Xunit;

namespace FootballTeamsRanking.Test
{
    public class RankingFacts
    {
        [Fact]
        public void TeamPositionMethodShouldReturnCorrectTeamRankingPosition()
        {
            Team team1 = new Team("Barcelona", 12);
            Team team2 = new Team("Real Madrid", 22);
            Team team3 = new Team("Bayern Munchen", 16);
            Team team4 = new Team("Argentina", 30);
            Team team5 = new Team("Brazil", 28);
            Team[] teams = { team4, team5, team2, team3, team1 };
            Ranking ranking = new Ranking(teams);
            Assert.Equal(1, ranking.TeamPosition(team4));
            Assert.Equal(2, ranking.TeamPosition(team5));
            Assert.Equal(3, ranking.TeamPosition(team2));
            Assert.Equal(4, ranking.TeamPosition(team3));
            Assert.Equal(5, ranking.TeamPosition(team1));
        }

        [Fact]
        public void UnrankedTeamShouldHaveIndexZero()
        {
            Team team1 = new Team("Barcelona", 12);
            Team team2 = new Team("Real Madrid", 22);
            Team team3 = new Team("Bayern Munchen", 16);
            Team team4 = new Team("Argentina", 30);
            Team team5 = new Team("Brazil", 28);
            Team team6 = new Team("Belgium", 13);
            Team[] teams = { team4, team5, team2, team3, team1 };
            Ranking ranking = new Ranking(teams);
            Assert.Equal(0, ranking.TeamPosition(team6));
        }

        [Fact]
        public void TeamInfoMethodShouldReturnCorrectTeam()
        {
            Team team1 = new Team("Barcelona", 12);
            Team team2 = new Team("Real Madrid", 22);
            Team team3 = new Team("Bayern Munchen", 16);
            Team team4 = new Team("Argentina", 30);
            Team team5 = new Team("Brazil", 28);
            Team team6 = new Team("Belgium", 13);
            Team[] teams = { team4, team5, team2, team3, team6, team1 };
            Ranking ranking = new Ranking(teams);
            Assert.Equal(team6, ranking.TeamInfo(5));
        }

        [Fact]
        public void UnrankedTeamShouldHaveNoInfo()
        {
            Team team1 = new Team("Barcelona", 12);
            Team team2 = new Team("Real Madrid", 22);
            Team team3 = new Team("Bayern Munchen", 16);
            Team team4 = new Team("Argentina", 30);
            Team team5 = new Team("Brazil", 28);
            Team[] teams = { team4, team5, team2, team3, team1 };
            Ranking ranking = new Ranking(teams);
            Team emptyTeam = new Team("", 0);
            Assert.Equal(0, ranking.TeamInfo(6).CompareNames(emptyTeam));
            Assert.Equal(0, ranking.TeamInfo(6).ComparePoints(emptyTeam));
        }

        [Fact]
        public void TeamInfoMethodShouldReturnNoInfoWhenRankingIsEmpty()
        {
            Team[] teams = {};
            Ranking ranking = new Ranking(teams);
            Team emptyTeam = new Team("", 0);
            Assert.Equal(0, ranking.TeamInfo(6).CompareNames(emptyTeam));
            Assert.Equal(0, ranking.TeamInfo(6).ComparePoints(emptyTeam));
        }

        [Fact]

        public void AddsNewTeamWhenRankingIsEmpty()
        {
            Team team = new Team("Argentina", 28);
            Team[] teams = { };
            Ranking ranking = new Ranking(teams);
            ranking.AddTeam(team);
            Assert.Equal(team, ranking.TeamInfo(1));
        }

        [Fact]
        public void NewTeamIsAddedToCorrectPosition()
        {
            Team team1 = new Team("Barcelona", 12);
            Team team2 = new Team("Real Madrid", 22);
            Team team3 = new Team("Bayern Munchen", 16);
            Team team4 = new Team("Argentina", 30);
            Team team5 = new Team("Brazil", 28);
            Team randomPointsTeam = new Team("Romania", 21);
            Team lowestPointsTeam = new Team("Belgium", 10);
            Team highestPointsTeam = new Team("Portugal", 31);
            Team[] teams = { team4, team5, team2, team3, team1 };
            Ranking ranking = new Ranking(teams);
            ranking.AddTeam(randomPointsTeam);
            ranking.AddTeam(lowestPointsTeam);
            ranking.AddTeam(highestPointsTeam);
            Assert.Equal(1, ranking.TeamPosition(highestPointsTeam));
            Assert.Equal(2, ranking.TeamPosition(team4));
            Assert.Equal(3, ranking.TeamPosition(team5));
            Assert.Equal(4, ranking.TeamPosition(team2));
            Assert.Equal(5, ranking.TeamPosition(randomPointsTeam));
            Assert.Equal(6, ranking.TeamPosition(team3));
            Assert.Equal(7, ranking.TeamPosition(team1));
            Assert.Equal(8, ranking.TeamPosition(lowestPointsTeam));
            
        }

        [Fact]
        public void TeamsShouldNotBeAddedTwice()
        {
            Team team1 = new Team("Barcelona", 12);
            Team team2 = new Team("Real Madrid", 22);
            Team team3 = new Team("Bayern Munchen", 16);
            Team team4 = new Team("Argentina", 30);
            Team team5 = new Team("Brazil", 28);
            Team team6 = new Team("Brazil", 12);
            Team[] teams = { team4, team5, team2, team3, team1 };
            Ranking ranking = new Ranking(teams);
            ranking.AddTeam(team6);
            Assert.Equal(0, ranking.TeamPosition(team6));
        }

        [Fact]
        public void TeamsWithSamePointsAreRankedBasedOnTheirName()
        {
            Team team1 = new Team("Barcelona", 12);
            Team team2 = new Team("Real Madrid", 12);
            Team team3 = new Team("Bayern Munchen", 16);
            Team team4 = new Team("Argentina", 30);
            Team team5 = new Team("Brazil", 28);
            Team teamAddedAtTheEnd = new Team("Romania", 12);
            Team teamAddedInTheBeginning = new Team("Ankara", 30);
            Team teamAddedInBetween = new Team("Bangladesh", 16);
            Team[] teams = { team4, team5, team3, team1, team2 };
            Ranking ranking = new Ranking(teams);
            ranking.AddTeam(teamAddedAtTheEnd);
            ranking.AddTeam(teamAddedInTheBeginning);
            ranking.AddTeam(teamAddedInBetween);
            Assert.Equal(1, ranking.TeamPosition(teamAddedInTheBeginning));
            Assert.Equal(2, ranking.TeamPosition(team4));
            Assert.Equal(3, ranking.TeamPosition(team5));
            Assert.Equal(4, ranking.TeamPosition(teamAddedInBetween));
            Assert.Equal(5, ranking.TeamPosition(team3));
            Assert.Equal(6, ranking.TeamPosition(team1));
            Assert.Equal(7, ranking.TeamPosition(team2));
            Assert.Equal(8, ranking.TeamPosition(teamAddedAtTheEnd));
            
           
        }

        [Fact]
        public void RankingIsUpdatedCorrectlyAfterAMatch()
        {
            Team team1 = new Team("Barcelona", 12);
            Team team2 = new Team("Real Madrid", 24);
            Team team3 = new Team("Bayern Munchen", 16);
            Team team4 = new Team("Argentina", 30);
            Team team5 = new Team("Brazil", 28);
            Team team6 = new Team("Romania", 21);
            Team team7 = new Team("Portugal", 31);
            Team[] teams = { team7, team4, team5, team2, team6, team3, team1 };
            Ranking ranking = new Ranking(teams);
            ranking.UpdateRanking(team1, team5, 7, 2);
            Assert.Equal(1, ranking.TeamPosition(team7));
            Assert.Equal(2, ranking.TeamPosition(team4));
            Assert.Equal(3, ranking.TeamPosition(team2));
            Assert.Equal(4, ranking.TeamPosition(team5));
            Assert.Equal(5, ranking.TeamPosition(team6));
            Assert.Equal(6, ranking.TeamPosition(team1));
            Assert.Equal(7, ranking.TeamPosition(team3));
        }

        [Fact]
        public void RankingStaysTheSameWhenTeamsDoNotAdvanceOrFallAfterAMatch()
        {
            Team team1 = new Team("Barcelona", 12);
            Team team2 = new Team("Real Madrid", 24);
            Team team3 = new Team("Bayern Munchen", 16);
            Team team4 = new Team("Argentina", 30);
            Team team5 = new Team("Brazil", 28);
            Team team6 = new Team("Romania", 21);
            Team team7 = new Team("Portugal", 31);
            Team[] teams = { team7, team4, team5, team2, team6, team3, team1 };
            Ranking ranking = new Ranking(teams);
            ranking.UpdateRanking(team7, team1, 1, 0);
            ranking.UpdateRanking(team2, team5, 1, 2);
            Assert.Equal(1, ranking.TeamPosition(team7));
            Assert.Equal(2, ranking.TeamPosition(team4));
            Assert.Equal(3, ranking.TeamPosition(team5));
            Assert.Equal(4, ranking.TeamPosition(team2));
            Assert.Equal(5, ranking.TeamPosition(team6));
            Assert.Equal(6, ranking.TeamPosition(team3));
            Assert.Equal(7, ranking.TeamPosition(team1));
        }

        [Fact]
        public void RankingIsUpdatedCorrectlyAfterManyMatches()
        {
            Team team1 = new Team("Barcelona", 12);
            Team team2 = new Team("Real Madrid", 24);
            Team team3 = new Team("Bayern Munchen", 16);
            Team team4 = new Team("Argentina", 30);
            Team team5 = new Team("Brazil", 28);
            Team team6 = new Team("Romania", 21);
            Team team7 = new Team("Portugal", 31);
            Team[] teams = { team7, team4, team5, team2, team6, team3, team1 };
            Ranking ranking = new Ranking(teams);
            ranking.UpdateRanking(team1, team5, 6, 3);
            ranking.UpdateRanking(team2, team4, 3, 5);
            ranking.UpdateRanking(team3, team6, 4, 4);
            ranking.UpdateRanking(team7, team4, 6, 7);
            ranking.UpdateRanking(team5, team1, 1, 0);
            ranking.UpdateRanking(team3, team7, 8, 1);
            Assert.Equal(1, ranking.TeamPosition(team4));
            Assert.Equal(2, ranking.TeamPosition(team5));
            Assert.Equal(3, ranking.TeamPosition(team3));
            Assert.Equal(4, ranking.TeamPosition(team7));
            Assert.Equal(5, ranking.TeamPosition(team2));
            Assert.Equal(6, ranking.TeamPosition(team6));
            Assert.Equal(7, ranking.TeamPosition(team1));
        }
    }
}
