using WWW_APP_PROJECT.Data.Enum;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.ViewModels
{
    public class TournamentsViewModel
    {
        public List<TeamTournament> MyTournaments { get; set; }
        public List<Tuple<ShareTrybe,TeamTournament>> SharedTournaments {get; set; }
    }
}
