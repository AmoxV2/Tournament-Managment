using WWW_APP_PROJECT.Data.Enum;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.ViewModels
{
    public class CreateTeamPlayerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public IFormFile Image { get; set; }
        public int TeamId { get; set; }
        public string? TeamName { get; set; }
    }
}
