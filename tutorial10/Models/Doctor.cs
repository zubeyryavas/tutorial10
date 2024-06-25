using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tutorial10.Models
{
    [Table("Doctor")]
    public class Doctor
    {
        [Key]
        public int IdDoctor { get; set; }

        [Required,MaxLength(100, ErrorMessage ="Not more than 100 letters")]
        public string FirstName { get; set; }

        [Required,MaxLength(100, ErrorMessage = "Not more than 100 letters")]
        public string LastName { get; set; }

        [Required,MaxLength(100, ErrorMessage = "Not more than 100 letters")]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
    }
}
