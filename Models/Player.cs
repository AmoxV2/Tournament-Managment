using System.ComponentModel.DataAnnotations;

namespace WWW_APP_PROJECT.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Surname { get; set; }

        [Required]
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        [Required]
        public bool Gender { get; set; }

        public virtual ICollection<Tournament>? Tournaments { get; set; }
    }
}
