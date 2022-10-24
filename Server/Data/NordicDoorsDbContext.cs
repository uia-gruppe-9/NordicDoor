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
            modelBuilder.Entity<UserTeam>().HasKey(ut => new { ut.EmployeeId, ut.TeamId });
        }

        public DbSet<Suggestion> Suggestions{ get; set; }
        public DbSet<Employee> Employees{ get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<UserTeam> UserTeam{ get; set; }

    }
}
