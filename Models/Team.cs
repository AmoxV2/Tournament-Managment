using System.ComponentModel.DataAnnotations;

namespace WWW_APP_PROJECT.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        public virtual ICollection<Tournament>? Tournaments { get; set; }
    }
}
