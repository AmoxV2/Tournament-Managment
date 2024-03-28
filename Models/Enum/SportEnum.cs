using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WWW_APP_PROJECT.Models.Enum
{
    public enum SportEnum
    {
        [Display(Name = "Piłka Nożna")]
        Football,

        [Display(Name = "Piłka ręczna")]
        Handball,

        [Display(Name = "Siatkówka")]
        Volleyball,

        [Display(Name = "Koszykówka")]
        Basketball,

        [Display(Name = "Badminton")]
        Badminton,

        [Display(Name = "Tenis")]
        Tennis,

        [Display(Name = "Hokej na lodzie/na trawie")]
        Hockey,

        [Display(Name = "Boks")]
        Boxing,

        [Display(Name = "Bieganie")]
        Running,

        [Display(Name = "Pływanie")]
        Swimming
    }
}
