using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WWW_APP_PROJECT.Data.Enum;

namespace WWW_APP_PROJECT.Models
{
    public class SharedTournament
    {
        [Key]
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ShareTrybe ShareTrybe { get; set; }
        [ForeignKey("TeamTournament")]
        public int? TeamTournamentId { get; set; }
        public TeamTournament? TeamTournament { get; set; }
        [ForeignKey("SoloTournament")]
        public int? SoloTournamentId { get; set; }
        public SoloTournament? SoloTournament { get; set; }
    }
}
