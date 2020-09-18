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
            return !TeamNameIsEmpty() &&
                TeamNameHasOnlyLetters() &&
                TeamNameLengthIsAtLeastFour();
        }

        internal void UpdateTeamPoints(int extraPoints)
        {
            this.points += extraPoints;
        }

        private bool TeamNameIsEmpty()
        {
            return this.name == "";
        }

        private bool TeamNameHasOnlyLetters()
        {
            for (int i = 0; i < this.name.Length; i++)
            {
                if (!char.IsLetter(this.name[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
