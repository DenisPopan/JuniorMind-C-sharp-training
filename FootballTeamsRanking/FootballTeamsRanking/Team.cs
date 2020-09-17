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

        public sbyte ComparePoints(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team));
            }

            if (this.points == team.points)
            {
                return 0;
            }

            if (this.points > team.points)
            {
                return 1;
            }

            return -1;
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
