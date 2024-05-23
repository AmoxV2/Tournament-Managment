using WWW_APP_PROJECT.Data;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.ViewModels
{
    public class ManageLeagueViewModel
    {
       
        public List<TeamMatch> Matches { get; set; }
        public TeamTournament Tournament { get; set; }
        public List<TeamScoreViewModel> TeamScores { get; set; }
    }
}
