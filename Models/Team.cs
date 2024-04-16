using System.ComponentModel.DataAnnotations.Schema;
using WWW_APP_PROJECT.Data.Enum;

namespace WWW_APP_PROJECT.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public TeamSportDiscipline TeamSportDiscipline { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<TeamToTournament> TeamTournaments { get; set; }
    }
}
