using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WWW_APP_PROJECT.Models.Enum
{
    public enum TournamentFormat
    {
        [Display(Name = "Drabinka pojedynczej eliminacji")]
        SingleEliminationBracket,

        [Display(Name = "Turniej kołowy")]
        RoundRobinTournament,

        [Display(Name = "Turniej szwajcarski")]
        SwissTournament
    }
}

