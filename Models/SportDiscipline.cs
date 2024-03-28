using System.ComponentModel.DataAnnotations;
using WWW_APP_PROJECT.Models.Enum;

namespace WWW_APP_PROJECT.Models
{
    public class SportDiscipline
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)] 
        public string DisciplineName { get; set; }

        [Required]
        public SportEnum SportsDiscipline { get; set; }

        [Required]
        [StringLength(100)]
        public string SportType { get; set; }
        public virtual ICollection<Tournament>? Tournaments { get; set; }
    }
}
