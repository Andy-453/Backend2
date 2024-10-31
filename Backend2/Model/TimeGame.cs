
namespace Backend2.Model
{
    public class TimeGame
    {
        public int TmeId { get; set; }
        public int UserId { get; set; }
        public int TimeSeconds { get; set; }
       

        public Users Users { get; set; }
    }
}
