using System;

namespace FootballTeamsRanking
{
    public class Ranking
    {
        Team[] teams;

        public Ranking(Team[] teams)
        {
            this.teams = teams;
        }

        public int TeamPosition(Team team)
        {
            return Array.IndexOf(teams, team) + 1;
        }

        public (string, int) TeamInfo(int teamPosition)
        {
            if (teamPosition > teams.Length || teamPosition < 1)
            {
                return ("", 0);
            }

            return (teams[teamPosition - 1].Name, teams[teamPosition - 1].Points);
        }

        public void AddTeam(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team));
            }

            int teamPosition = 0;
            while (teamPosition < teams.Length && teams[teamPosition].Points > team.Points)
            {
                teamPosition++;
            }

            int newSize = teams.Length + 1;
            Array.Resize(ref teams, newSize);
            for (int i = teams.Length - 1; i > teamPosition; i--)
            {
                teams[i] = teams[i - 1];
            }

            teams[teamPosition] = team;
        }
    }
}
