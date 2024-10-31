namespace Backend2.Model
{
    public class Points
    {
        public int PointId { get; set; }
        public int UserId { get; set; }
        public int Point { get; set; }
       

        public Users  Users { get; set; }
    }
}
