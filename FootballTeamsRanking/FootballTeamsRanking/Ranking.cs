using System;
using System.Linq;

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

            if (AreNamesDuplicated(teams, team))
            {
                return;
            }

            int teamPosition = teams.Length == 0 ? 0 : BinarySearchTeamPosition(teams, team);
            int newSize = teams.Length + 1;
            Array.Resize(ref teams, newSize);
            RearrangeTeams(teams, teams.Length - 1, teamPosition);
            teams[teamPosition] = team;
        }

        public void UpdateRanking(Team firstTeam, Team secondTeam, int firstTeamScore, int secondTeamScore)
        {
            if (firstTeamScore == secondTeamScore)
            {
                return;
            }

            if (firstTeam == null)
            {
                throw new ArgumentNullException(nameof(firstTeam));
            }

            if (secondTeam == null)
            {
                throw new ArgumentNullException(nameof(secondTeam));
            }

            int scoreDifference = Math.Abs(firstTeamScore - secondTeamScore);
            Team winnerTeam;
            Team loserTeam;
            (winnerTeam, loserTeam) = DetermineWinnerAndLoserTeam(firstTeam, secondTeam, firstTeamScore, secondTeamScore);
            UpdatePoints(winnerTeam, loserTeam, scoreDifference);
            int currentWinnerTeamPosition = TeamPosition(winnerTeam) - 1;
            int currentLoserTeamPosition = TeamPosition(loserTeam) - 1;
            DeleteTeam(ref teams, currentWinnerTeamPosition);
            AddTeam(winnerTeam);
            DeleteTeam(ref teams, currentLoserTeamPosition);
            AddTeam(loserTeam);
        }

        private bool AreNamesDuplicated(Team[] teams, Team team)
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

        private void DeleteTeam(ref Team[] teams, int teamToDeletePosition)
        {
            teams = teams.Where((e, i) => i != teamToDeletePosition).ToArray();
        }

        private void RearrangeTeams(Team[] teams, int startPosition, int stopPosition)
        {
            for (int i = startPosition; i > stopPosition; i--)
            {
                teams[i] = teams[i - 1];
            }
        }

        private (Team, Team) DetermineWinnerAndLoserTeam(Team firstTeam, Team secondTeam, int firstTeamScore, int secondTeamScore)
        {
            if (firstTeamScore > secondTeamScore)
            {
                return (firstTeam, secondTeam);
            }

            return (secondTeam, firstTeam);
        }

        private void UpdatePoints(Team winnerTeam, Team loserTeam, int points)
        {
            winnerTeam.UpdateTeamPoints(points);
            loserTeam.UpdateTeamPoints(-points);
        }

        private int BinarySearchTeamPosition(Team[] teams, Team searchedTeam)
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
