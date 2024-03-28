using System.ComponentModel.DataAnnotations;

namespace WWW_APP_PROJECT.Models
{
    public class SportDiscipline
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)] 
        public string DisciplineName { get; set; }
        public virtual ICollection<Tournament>? Tournaments { get; set; }
        
    }
}
