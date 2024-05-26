using WWW_APP_PROJECT.Data.Enum;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.ViewModels
{
    public class CreateTeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public TeamSportDiscipline TeamSportDiscipline { get; set; }
        public string AppUserId { get; set; }
    }
}
