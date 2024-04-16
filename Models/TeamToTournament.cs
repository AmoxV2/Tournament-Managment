using System.ComponentModel.DataAnnotations.Schema;

namespace WWW_APP_PROJECT.Models
{
    public class TeamToTournament
    {
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Team Team { get; set; }
        [ForeignKey("TeamTournament")]
        public int TeamTournamentId { get; set; }
        public TeamTournament TeamTournament { get; set; }
    }
}
