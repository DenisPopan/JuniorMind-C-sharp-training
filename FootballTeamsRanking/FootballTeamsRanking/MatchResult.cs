namespace FootballTeamsRanking
{
    public class MatchResult
    {
        readonly Team team1;
        readonly Team team2;
        readonly int team1Score;
        readonly int team2Score;

        public MatchResult(Team team1, Team team2, int team1Score, int team2Score)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.team1Score = team1Score;
            this.team2Score = team2Score;
        }

        internal void UpdatePoints()
        {
            if (team1Score > team2Score)
            {
                team1.UpdateTeamPoints(3);
            }
            else if (team1Score < team2Score)
            {
                team2.UpdateTeamPoints(3);
            }
            else
            {
                team1.UpdateTeamPoints(1);
                team2.UpdateTeamPoints(1);
            }
        }
    }
}
