using WWW_APP_PROJECT.Data.Enum;

namespace WWW_APP_PROJECT.ViewModels
{
    public class CreateShareViewModel
    {
        public string? tournamentName { get; set; }
        public string ShareUser { get; set; }
        public int tournamentId { get; set; }
        public ShareTrybe ShareTrybe { get; set; }
    }
}
