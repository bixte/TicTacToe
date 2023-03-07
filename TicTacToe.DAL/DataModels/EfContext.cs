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
    }
}
