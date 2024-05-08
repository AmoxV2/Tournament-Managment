using Microsoft.EntityFrameworkCore;
using WWW_APP_PROJECT.Data;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }
    }
}
