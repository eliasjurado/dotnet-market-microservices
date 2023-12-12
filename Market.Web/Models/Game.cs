namespace Market.Web.Models
{
    public record Game
    {
        public int Year { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Console { get; set; }
        public Game(int year, string name, string publisher, string console)
        {
            Year = year;
            Name = name;
            Publisher = publisher;
            Console = console;
        }
    }
}
