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

            int scoreDifference = Math.Abs(firstTeamScore - secondTeamScore);
            Team winnerTeam;
            Team loserTeam;
            if (firstTeamScore > secondTeamScore)
            {
                winnerTeam = firstTeam;
                loserTeam = secondTeam;
            }
            else if (firstTeamScore < secondTeamScore)
            {
                winnerTeam = secondTeam;
                loserTeam = firstTeam;
            }
            else
            {
                return;
            }

            Team winnerTeamCopy = new Team(winnerTeam.Name, winnerTeam.Points + scoreDifference);
            Team loserTeamCopy = new Team(loserTeam.Name, loserTeam.Points - scoreDifference);

            int currentWinnerTeamPosition = TeamPosition(winnerTeam) - 1;
            int currentLoserTeamPosition = TeamPosition(loserTeam) - 1;

            int newWinnerTeamPosition = BinarySearchTeamPosition(teams, winnerTeamCopy);

            for (int i = currentWinnerTeamPosition; i > newWinnerTeamPosition; i--)
            {
                teams[i] = teams[i - 1];
            }

            teams[newWinnerTeamPosition] = winnerTeam;
            UpdatePoints(winnerTeam, scoreDifference);

            int newLoserTeamPosition = BinarySearchTeamPosition(teams, loserTeamCopy) - 1;

            for (int i = currentLoserTeamPosition; i < newLoserTeamPosition; i++)
            {
                teams[i] = teams[i + 1];
            }

            teams[newLoserTeamPosition] = loserTeam;
            UpdatePoints(loserTeam, -scoreDifference);
        }

        private void UpdatePoints(Team team, int points)
        {
            team.Points += points;
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
