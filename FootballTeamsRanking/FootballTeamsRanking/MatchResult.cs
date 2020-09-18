namespace FootballTeamsRanking
{
    class MatchResult
    {
        readonly Team winnerTeam;
        readonly Team loserTeam;
        readonly int winnerTeamScore;
        readonly int loserTeamScore;

        public MatchResult(Team team1, Team team2, int team1Score, int team2Score)
        {
            (winnerTeam, loserTeam, winnerTeamScore, loserTeamScore) = DetermineWinnerAndLoserTeam(team1, team2, team1Score, team2Score);
        }

        internal void UpdatePoints()
        {
            int scoreDifference = winnerTeamScore - loserTeamScore;
            if (scoreDifference == 0)
            {
                return;
            }

            winnerTeam.UpdateTeamPoints(scoreDifference);
            loserTeam.UpdateTeamPoints(-scoreDifference);
        }

        private (Team, Team, int, int) DetermineWinnerAndLoserTeam(Team firstTeam, Team secondTeam, int firstTeamScore, int secondTeamScore)
        {
            if (firstTeamScore > secondTeamScore)
            {
                return (firstTeam, secondTeam, firstTeamScore, secondTeamScore);
            }

            return (secondTeam, firstTeam, secondTeamScore, firstTeamScore);
        }
    }
}
