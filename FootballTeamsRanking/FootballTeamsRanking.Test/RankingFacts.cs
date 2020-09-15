using System;
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
            Assert.Equal((team6.Name, team6.Points), ranking.TeamInfo(5));
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
            Assert.Equal(("", 0), ranking.TeamInfo(6));
        }

        [Fact]

        public void AddsNewTeamWhenRankingIsEmpty()
        {
            Team team = new Team("Argentina", 28);
            Team[] teams = { };
            Ranking ranking = new Ranking(teams);
            ranking.AddTeam(team);
            Assert.Equal(("Argentina", 28), ranking.TeamInfo(1));
        }

    }
}
