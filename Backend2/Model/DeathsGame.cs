namespace Backend2.Model
{
    public class DeathsGame
    {
        public int DeathsId { get; set; }
        public int UserId { get; set; }
        public int Numberdeaths { get; set; }
       
        public Users Users { get; set; }
    }
}
