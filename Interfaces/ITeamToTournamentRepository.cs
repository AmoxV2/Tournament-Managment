using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.Interfaces
{
    public interface ITeamToTournamentRepository
    {
        TeamToTournament GetById(int teamId, int tournamentId);
 
        bool Add(TeamToTournament teamToTournament);
        bool Delete(TeamToTournament teamToTournament);
        bool Save();
        
    }
}
