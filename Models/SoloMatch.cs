using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WWW_APP_PROJECT.Data.Enum;

namespace WWW_APP_PROJECT.Models
{
    public class SoloMatch
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SoloPlayer")]
        public int HostPlayerId { get; set; }
        public SoloPlayer HostPlayer { get; set; }

        [ForeignKey("SoloPlayer")]
        public int GuestPlayerId { get; set; }
        public SoloPlayer GuestPlayer { get; set; }
        public int? HostScore { get; set; }
        public int? GuestScore { get; set; }
        public MatchResult MatchResult { get; set; }
        public DateTime Date { get; set; }
        public int Stage { get; set; }
        [ForeignKey("SoloTournament")]
        public int SoloTournamentId { get; set; }
        public SoloTournament SoloTournament { get; set; }
    }
}
