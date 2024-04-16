using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<TeamToTournament> TeamToTournaments { get; set; }
        public DbSet<TeamTournament> TeamTournaments { get; set; }
        public DbSet<SharedTournament> SharedTournaments { get; set; }
        public DbSet<TeamMatch> TeamMatches { get; set; }
        public DbSet<SoloMatch> SoloMatches { get; set; }
        public DbSet<SoloPlayer> SoloPlayers { get; set; }
        public DbSet<SoloTournament> SoloTournaments { get; set; }
        public DbSet<PlayerToTournament> PlayerToTournaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TeamToTournament>()
                .HasKey(tt => new { tt.TeamId, tt.TeamTournamentId });

            modelBuilder.Entity<TeamToTournament>()
                .HasOne(tt => tt.Team)
                .WithMany(t => t.TeamTournaments)
                .HasForeignKey(tt => tt.TeamId);

            modelBuilder.Entity<TeamToTournament>()
                .HasOne(tt => tt.TeamTournament)
                .WithMany(t => t.Teams)
                .HasForeignKey(tt => tt.TeamTournamentId);
            
        }
    }
}
