using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Interfaces
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<TeamTournament>> GetUserTournaments();
        Task<TeamTournament> GetByIdAsync(int id);
        Task<Team> GetTeamByIdAsync(int id);
        Task<List<Team>> GetTeams(int tournamentId);
        bool hasTorunamentStarted(int tournamentId);

        bool Add(TeamTournament tournament);
        bool Update(TeamTournament tournament);
        bool Delete(TeamTournament tournament);
        bool Save();
        
        
    }
}
