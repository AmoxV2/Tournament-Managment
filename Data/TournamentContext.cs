using Microsoft.EntityFrameworkCore;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Data
{
    public class TournamentContext : DbContext
    {
        public TournamentContext(DbContextOptions options) : base(options) { }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<SportDiscipline> SportsDisciplines { get; set; }
    }
}
