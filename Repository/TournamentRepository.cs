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
            throw new NotImplementedException();
        }

        public async Task<TeamTournament> GetByIdAsync(int id)
        {
            return await _context.TeamTournaments.FirstOrDefaultAsync(i => i.Id == id);
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(TeamTournament tournament)
        {
            throw new NotImplementedException();
        }
    }
}
