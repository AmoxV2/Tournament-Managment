using WWW_APP_PROJECT.Models;

namespace WWW_APP_PROJECT.ViewModels
{
    public class EditTournamentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
       
        public IFormFile? Image { get; set; }
        public Address Address { get; set; }
        public bool IsPublic { get; set; }
       
    }
}
