using System;

namespace FootballTeamsRanking
{
    public class Ranking
    {
        readonly Team[] teams;

        public Ranking(Team[] teams)
        {
            this.teams = teams;
        }

        public int TeamPosition(Team team)
        {
            return Array.IndexOf(teams, team) + 1;
        }
    }
}
