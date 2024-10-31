using Backend2.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend2.Context
{
    public class JuegoDbContext : DbContext
    {
        
        public JuegoDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Points> Points { get; set; }
        public DbSet<TimeGame> TimeGames { get; set; }
        public DbSet<DeathsGame> DeathsGame { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<Points>()
                .HasKey(u => u.PointId);


            modelBuilder.Entity<TimeGame>()
                .HasKey(u => u.TmeId);

            modelBuilder.Entity<DeathsGame>()
                .HasKey(u => u.DeathsId);

        
            //relaciones
            modelBuilder.Entity<Users>()
                .HasMany(u => u.Points)
                .WithOne(p => p.Users)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(u => u.TimeGames)
                .WithOne(t => t.Users)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(u => u.DeadsGames)
                .WithOne(m => m.Users
                )
                .HasForeignKey(m => m.UserId);
        }

    }
}
