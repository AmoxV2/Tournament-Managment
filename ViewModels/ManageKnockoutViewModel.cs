using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.ViewModels
{
    public class ManageKnockoutViewModel
    {
        public List<List<TeamMatch>> Rounds { get; set; }
        public TeamTournament Tournament { get; set; }
    }
}
