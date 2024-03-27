using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WWW_APP_PROJECT.Models
{
    public enum TournamentFormat
    {
        [Display(Name = "Drabinka pojedynczej eliminacji")]
        SingleEliminationBracket,

        [Display(Name = "Turniej kołowy")]
        RoundRobinTournament,

        [Display(Name = "Turniej szwajcarski")]
        SwissTournament
    }
    public class Tournament
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TourmanentName { get; set; }

        [Required]
        public TournamentFormat TournamentFormat { get; set; }

        [Required]
        [StringLength(100)]
        public string TournamentPlace { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime? TournamentStartDate { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime? TournamentEndDate { get; set; }

        [Required]
        public int SportDisciplineId { get; set; }
        public SportDiscipline SportDiscipline { get; set; }
    }
}
