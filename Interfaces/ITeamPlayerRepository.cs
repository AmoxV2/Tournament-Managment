using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Interfaces
{
    public interface ITeamPlayerRepository
    {
        Task<TeamPlayer> GetByIdAsync(int id);
        Task<IEnumerable<TeamPlayer>> GetPlayersByTeam(int id);
        bool Add(TeamPlayer player);
        bool Update(TeamPlayer player);
        bool Delete(TeamPlayer player);
        bool Save();
    }
}
