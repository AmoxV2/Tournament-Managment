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

        public Task<TeamTournament> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
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
