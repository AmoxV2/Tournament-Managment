using Microsoft.EntityFrameworkCore;
using WWW_APP_PROJECT.Data;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Repository
{
    public class TeamPlayerRepository : ITeamPlayerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TeamPlayerRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool Add(TeamPlayer player)
        {
            _context.Add(player);
            return Save();
        }

        public bool Delete(TeamPlayer player)
        {
            _context.Remove(player);
            return Save();
        }

        public async Task<TeamPlayer> GetByIdAsync(int id)
        {
            return await _context.TeamPlayers.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<TeamPlayer>> GetPlayersByTeam(int id)
        {
            var teamPlayers = _context.TeamPlayers.Where(p => p.Team.Id == id);
            return teamPlayers.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(TeamPlayer player)
        {
            _context.Update(player);
            return Save();
        }
    }
}
