namespace Backend2.Model
{
    public class Users
    {
        public int UserId { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }

        public ICollection<Points> Points { get; set; }
        public ICollection<TimeGame> TimeGames { get; set; }
        public ICollection<DeathsGame> DeadsGames { get; set; }
    }
}
