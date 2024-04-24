using Microsoft.AspNetCore.Identity;

namespace WWW_APP_PROJECT.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<TeamTournament> TeamTournaments { get; set; }
        public ICollection<SoloTournament> SoloTournaments { get; set; }
        public ICollection<SharedTournament> SharedTournaments { get; set; }
    }
}
