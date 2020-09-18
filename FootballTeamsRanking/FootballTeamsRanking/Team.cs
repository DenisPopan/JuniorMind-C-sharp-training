using System;

namespace FootballTeamsRanking
{
    public class Team
    {
        readonly string name;
        int points;

        public Team(string name, int points)
        {
            this.name = name;
            this.points = points;
        }

        public int ComparePoints(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team));
            }

            return this.points.CompareTo(team.points);
        }

        public int CompareNames(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team));
            }

            return string.Compare(this.name, team.name);
        }

        internal void UpdateTeamPoints(int extraPoints)
        {
            this.points += extraPoints;
        }
    }
}
