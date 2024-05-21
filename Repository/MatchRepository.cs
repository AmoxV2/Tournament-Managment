using Microsoft.EntityFrameworkCore;
using WWW_APP_PROJECT.Data;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Repository
{
    public class MatchRepository : IMatchRepository
    {
        private readonly ApplicationDbContext _context;
       
        public MatchRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(TeamMatch match)
        {
            _context.Add(match);
            return Save();
        }

        public async Task<List<TeamMatch>> GetMatchesByTournament(int tournamentId)
        {
            return await _context.TeamMatches.Include(h=>h.HostTeam).Include(g=>g.GuestTeam).Where(c => c.TeamTournamentId == tournamentId).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
