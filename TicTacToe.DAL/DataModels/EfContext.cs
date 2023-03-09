using Microsoft.EntityFrameworkCore;

namespace TicTacToe.DataModels.DAL
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().Navigation(r => r.PlayerX).AutoInclude();
            modelBuilder.Entity<Room>().Navigation(r => r.Player0).AutoInclude();
            modelBuilder.Entity<Room>().Navigation(r => r.PlayerWin).AutoInclude();
            modelBuilder.Entity<Room>().Navigation(r => r.Steps).AutoInclude();
            modelBuilder.Entity<Room>().HasMany(r => r.Steps).WithOne().OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
