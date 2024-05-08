namespace WWW_APP_PROJECT.ViewModels
{
    public class EditUserDashboardViewModel
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? UserName { get; set; }
        public IFormFile Image { get; set; }
    }
}
