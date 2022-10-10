using Microsoft.EntityFrameworkCore;
using Nordic_Door.Shared.Models.Database;

namespace Nordic_Door.Server.Data
{
    public class NordicDoorsDbContext : DbContext
{
    public NordicDoorsDbContext(DbContextOptions options) : base(options)
    {

    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTeams>().HasNoKey();
        }

        public DbSet<Suggestions> Suggestions{ get; set; }
        public DbSet<Employees> Employees{ get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<UserTeams> UserTeams{ get; set; }

    }
}
