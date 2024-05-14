using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WWW_APP_PROJECT.Data.Enum;
using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.ViewModels
{
    public class CreateTournamentViewModel
    {
    
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TournamentType TournamentType { get; set; }
        public int NumberOfTeams { get; set; }
        public TeamSportDiscipline TeamSportDiscipline { get; set; }
        public IFormFile? Image { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public bool IsPublic { get; set; }
        public string AppUserId { get; set; }
        

    }
}
