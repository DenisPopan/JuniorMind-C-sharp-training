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

        public bool NameIsValid()
        {
            return !TeamNameIsEmpty();
        }

        internal void UpdateTeamPoints(int extraPoints)
        {
            this.points += extraPoints;
        }

        private bool TeamNameIsEmpty()
        {
            return this.name == "";
        }
    }
}
