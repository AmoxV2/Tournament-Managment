using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetUserTeams();
        Task<Team> GetByIdAsync(int id);
        bool Add(Team team);
        bool Update(Team team);
        bool Delete(Team team);
        bool Save();
    }
}
