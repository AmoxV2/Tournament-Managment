using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Data
{
    public class TournamentContext : IdentityDbContext
    {
        public TournamentContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<SportDiscipline> SportsDisciplines { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
