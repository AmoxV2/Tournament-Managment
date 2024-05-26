using Microsoft.EntityFrameworkCore;
using WWW_APP_PROJECT.Data;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Repository
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public TournamentRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            
        }
        public bool Add(TeamTournament tournament)
        {
            _context.Add(tournament);
            return Save();
        }

        public bool Delete(TeamTournament tournament)
        {
            var teamToTournaments = _context.TeamToTournaments.Where(c => c.TeamTournamentId == tournament.Id);
            var teamMatches = _context.TeamMatches.Where(c => c.TeamTournamentId == tournament.Id);
            var sharedTournaments = _context.SharedTournaments.Where(c => c.TeamTournamentId == tournament.Id);
            foreach (var item in teamToTournaments)
            {
                _context.Remove(item);
            }
            foreach (var item in teamMatches)
            {
                _context.Remove(item);
            }
            foreach (var item in sharedTournaments)
            {
                _context.Remove(item);
            }
            _context.Remove(tournament);
            return Save();
        }

        public async Task<TeamTournament> GetByIdAsync(int id)
        {
            return await _context.TeamTournaments.Include(a=>a.WinnerTeam).Include(a=>a.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Team>> GetTeams(int tournamentId)
        {
            List<TeamToTournament> teamToTournaments = await _context.TeamToTournaments.Where(c => c.TeamTournamentId == tournamentId).ToListAsync();
            List<Team> teams = new List<Team>();
            foreach (var item in teamToTournaments)
            {
                teams.Add(await GetTeamByIdAsync(item.TeamId));
            }
            return teams;
        }
        public async Task<Team> GetTeamByIdAsync(int id)
        {
            return await _context.Teams.Include(t => t.TeamPlayers).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<TeamTournament>> GetUserTournaments()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userTournaments = _context.TeamTournaments.Where(c => c.AppUser.Id == curUser);
            return userTournaments.ToList();
        }

        public bool hasTorunamentStarted(int tournamentId)
        {
            var match = _context.TeamMatches.FirstOrDefault(i => i.TeamTournamentId == tournamentId);
            if (match != null) return true;
            else return false;

        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(TeamTournament tournament)
        {
            _context.Update(tournament);
            return Save();
        }
    }
}
