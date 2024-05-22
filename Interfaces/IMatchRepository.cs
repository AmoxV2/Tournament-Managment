using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Interfaces
{
    public interface IMatchRepository
    {
        bool Add(TeamMatch match);
        bool Save();
        Task<List<TeamMatch>> GetMatchesByTournament(int tournamentId);

        bool Update(TeamMatch match);
        Task<TeamMatch> GetMatchById(int id);
    }
}
