using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WWW_APP_PROJECT.Data.Enum;

namespace WWW_APP_PROJECT.Models
{
    public class TeamTournament
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TournamentType TournamentType { get; set; }
        public int NumberOfTeams { get; set; }
        public TeamSportDiscipline TeamSportDiscipline { get; set; }
        public string? ImageUrl { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public bool IsPublic { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        [ForeignKey("Team")]
        public int? WinnerTeamId { get; set; }
        public Team? WinnerTeam { get; set; }

        public ICollection<TeamToTournament> Teams { get; set; }
        public ICollection<TeamMatch> Matches { get; set; }
        public ICollection<SharedTournament> SharedTournaments { get; set; }

    }
}
