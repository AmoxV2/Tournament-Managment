using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.ViewModels
{
    public class AddTeamsTournamentViewModel
    {
        public List<Team> TournamentTeams { get; set; }
        public List<Team> AvailableTeams { get; set; }
        public TeamTournament Tournament { get; set; }
        
    }
}
