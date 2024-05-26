using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Interfaces
{
    public interface IShareRepository
    {
        bool Add(SharedTournament sharedTournament);
        bool Delete(SharedTournament sharedTournament);
        bool Save();
        Task<SharedTournament> GetById(int id);
        Task<List<SharedTournament>> GetUserShares(string userId);
        Task<List<SharedTournament>> GetTournamentShares(int tournamentId);
        Task<List<SharedTournament>> GetMyShares(string userId);

    }
}
