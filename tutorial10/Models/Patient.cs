using System.ComponentModel.DataAnnotations;

namespace tutorial10.Models
{
    public class Patient
    {
        [Key]
        public int IdPatient { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Not more than 100 letters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Not more than 100 letters")]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();

    }
}
