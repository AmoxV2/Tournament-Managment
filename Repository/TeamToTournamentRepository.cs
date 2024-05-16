using Microsoft.EntityFrameworkCore;
using WWW_APP_PROJECT.Data;
using WWW_APP_PROJECT.Interfaces;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Repository
{
    public class TeamToTournamentRepository : ITeamToTournamentRepository
    {
        private readonly ApplicationDbContext _context;
        
        public TeamToTournamentRepository(ApplicationDbContext context)
        {
            _context = context;
            

        }
        public bool Add(TeamToTournament teamToTournament)
        {
            _context.Add(teamToTournament);
            return Save();
        }

        public bool Delete(TeamToTournament teamToTournament)
        {
            _context.Remove(teamToTournament);
            return Save();
        }

        public TeamToTournament GetById(int teamId, int tournamentId)
        {
            return _context.TeamToTournaments.FirstOrDefault(i => i.TeamId == teamId && i.TeamTournamentId == tournamentId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
