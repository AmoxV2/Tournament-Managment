using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WWW_APP_PROJECT.Models
{
    public class PlayerToTournament
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("SoloPlayer")]
        public int SoloPlayerId { get; set; }
        public SoloPlayer Player { get; set; }
        [ForeignKey("SoloTournament")]
        public int SoloTournamentId { get; set; }
        public SoloTournament SoloTournament { get; set; }
    }
}
