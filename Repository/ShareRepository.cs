using Microsoft.EntityFrameworkCore;
using WWW_APP_PROJECT.Data;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Repository
{
    public class ShareRepository : IShareRepository
    {
        ApplicationDbContext _context;
        public ShareRepository(ApplicationDbContext contex)
        {
            _context = contex;
        }
        public bool Add(SharedTournament sharedTournament)
        {
            _context.SharedTournaments.Add(sharedTournament);
            return Save();
        }

        public bool Delete(SharedTournament sharedTournament)
        {
            _context.SharedTournaments.Remove(sharedTournament);
            return Save();
        }

        public async Task<List<SharedTournament>> GetTournamentShares(int tournamentId)
        {
            return await _context.SharedTournaments.Include(t => t.TeamTournament).Include(t => t.AppUser).Where(x => x.TeamTournamentId == tournamentId).ToListAsync();
        }

        public async Task<List<SharedTournament>> GetUserShares(string userId)
        {
            return await _context.SharedTournaments.Where(x => x.AppUserId == userId).ToListAsync();
        }
        public async Task<List<SharedTournament>> GetMyShares(string userId)
        {
            var myTournaments = await _context.TeamTournaments.Where(x => x.AppUserId == userId).ToListAsync();
            List<SharedTournament> sharedTournaments = new List<SharedTournament>();
            foreach (var item in myTournaments)
            {
                var shares = await GetTournamentShares(item.Id);
                sharedTournaments.AddRange(shares);
            }
            return sharedTournaments;
        }
        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public async Task<SharedTournament> GetById(int id)
        {
            return await _context.SharedTournaments.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
