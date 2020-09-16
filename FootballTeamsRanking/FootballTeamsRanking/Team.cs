namespace FootballTeamsRanking
{
    public class Team
    {
        readonly string name;

        public Team(string name, int points)
        {
            this.name = name;
            this.Points = points;
        }

        public string Name
        {
            get => name;
        }

        public int Points { get; set; }
    }
}
