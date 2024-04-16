using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WWW_APP_PROJECT.Data.Enum;

namespace WWW_APP_PROJECT.Models
{
    public class TeamMatch
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Team")]
        public int HostTeamId { get; set; }
        public Team HostTeam { get; set; }

        [ForeignKey("Team")]
        public int GuestTeamId { get; set; }
        public Team GuestTeam { get; set; }
        public int? HostScore { get; set; }
        public int? GuestScore { get; set; }
        public MatchResult MatchResult { get; set; }
        public DateTime Date { get; set; }
        public int Stage { get; set; }
        [ForeignKey("TeamTournament")]
        public int TeamTournamentId { get; set; }
        public TeamTournament TeamTournament { get; set; }

    }
}

