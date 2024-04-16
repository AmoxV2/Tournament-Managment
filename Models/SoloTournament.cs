using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WWW_APP_PROJECT.Data.Enum;

namespace WWW_APP_PROJECT.Models
{
    public class SoloTournament
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TournamentType TournamentType { get; set; }
        public int NumberOfPlayers { get; set; }
        public SoloSportDiscipline TeamSportDiscipline { get; set; }
        public string? ImageUrl { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public bool IsPublic { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        [ForeignKey("SoloPlayers")]
        public int? WinnerId { get; set; }
        public SoloPlayer? Winner { get; set; }

        public ICollection<PlayerToTournament> Players { get; set; }
        public ICollection<SoloMatch> Matches { get; set; }
        public ICollection<SharedTournament> SharedTournaments { get; set; }
    }
}
