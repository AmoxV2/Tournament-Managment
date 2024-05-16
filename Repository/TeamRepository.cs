using Microsoft.EntityFrameworkCore;
using WWW_APP_PROJECT.Data;
using WWW_APP_PROJECT.Data.Enum;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TeamRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool Add(Team team)
        {
            _context.Add(team);
            return Save();
        }

        public bool Delete(Team team)
        {
            throw new NotImplementedException();
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            return await _context.Teams.Include(t => t.TeamPlayers).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Team>> GetTeamsBySport(TeamSportDiscipline sportDiscipline)
        {
            return await _context.Teams.Where(c => c.TeamSportDiscipline == sportDiscipline).ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetUserTeams()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userTeams = _context.Teams.Where(c => c.AppUser.Id == curUser);
            return userTeams.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Team team)
        {
            _context.Update(team);
            return Save();
        }
    }
}
