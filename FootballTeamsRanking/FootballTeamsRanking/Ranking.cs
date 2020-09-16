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

            int teamPosition = teams.Length == 0 ? 0 : BinarySearchTeamPosition(teams, team);
            int newSize = teams.Length + 1;
            Array.Resize(ref teams, newSize);
            for (int i = teams.Length - 1; i > teamPosition; i--)
            {
                teams[i] = teams[i - 1];
            }

            teams[teamPosition] = team;
        }

        private int BinarySearchTeamPosition(Team[] teams, Team searchedTeam)
        {
            if (searchedTeam.Points > teams[0].Points)
            {
                return 0;
            }

            if (searchedTeam.Points < teams[teams.Length - 1].Points)
            {
                return teams.Length;
            }

            int left = 0;
            int right = teams.Length - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (searchedTeam.Points == teams[mid].Points)
                {
                    if (string.Compare(searchedTeam.Name, teams[mid].Name) == -1)
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }
                else if (searchedTeam.Points > teams[mid].Points)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return left;
        }
    }
}
