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

        public Team TeamInfo(int teamPosition)
        {
            if (teamPosition > teams.Length || teamPosition < 1)
            {
                return new Team("", 0);
            }

            return teams[teamPosition - 1];
        }

        public void AddTeam(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team));
            }

            if (AreNamesDuplicated(team))
            {
                return;
            }

            int teamPosition = teams.Length == 0 ? 0 : BinarySearchTeamPosition(team);
            int newSize = teams.Length + 1;
            Array.Resize(ref teams, newSize);
            ShiftTeamsToTheRight(teams.Length - 1, teamPosition);
            teams[teamPosition] = team;
        }

        public void UpdateRanking(Team firstTeam, Team secondTeam, int firstTeamScore, int secondTeamScore)
        {
            if (firstTeam == null)
            {
                throw new ArgumentNullException(nameof(firstTeam));
            }

            if (secondTeam == null)
            {
                throw new ArgumentNullException(nameof(secondTeam));
            }

            MatchResult matchResult = new MatchResult(firstTeam, secondTeam, firstTeamScore, secondTeamScore);
            matchResult.UpdatePoints();
            SortTeams();
        }

        private void SortTeams()
        {
            bool teamsAreSorted;
            do
            {
                teamsAreSorted = true;
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    if (teams[i].ComparePoints(teams[i + 1]) == -1 ||
                        (teams[i].ComparePoints(teams[i + 1]) == 0 && teams[i].CompareNames(teams[i + 1]) == 1))
                    {
                        Swap(ref teams[i], ref teams[i + 1]);
                        teamsAreSorted = false;
                    }
                }
            }
            while (!teamsAreSorted);
        }

        private void Swap(ref Team team1, ref Team team2)
        {
            var temp = team1;
            team1 = team2;
            team2 = temp;
        }

        private bool AreNamesDuplicated(Team team)
        {
            for (int i = 0; i < teams.Length; i++)
            {
                if (team.CompareNames(teams[i]) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void ShiftTeamsToTheRight(int startPosition, int stopPosition)
        {
            for (int i = startPosition; i > stopPosition; i--)
            {
                teams[i] = teams[i - 1];
            }
        }

        private int BinarySearchTeamPosition(Team searchedTeam)
        {
            if (searchedTeam.ComparePoints(teams[0]) == 1)
            {
                return 0;
            }

            if (searchedTeam.ComparePoints(teams[teams.Length - 1]) == -1)
            {
                return teams.Length;
            }

            int left = 0;
            int right = teams.Length - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (searchedTeam.ComparePoints(teams[mid]) == 0)
                {
                    if (searchedTeam.CompareNames(teams[mid]) == -1)
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }
                else if (searchedTeam.ComparePoints(teams[mid]) == 1)
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
