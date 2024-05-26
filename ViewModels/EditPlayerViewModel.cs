namespace WWW_APP_PROJECT.ViewModels
{
    public class EditPlayerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int TeamId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
