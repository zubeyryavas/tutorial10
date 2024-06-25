using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tutorial10.Models
{
    [Table("Prescription")]
    public class Prescription
    {
        [Key]
        public int Idprescription { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        
        public int IdPatient { get; set; }
        [ForeignKey("IdPatient")]
        public Patient Patient { get; set; }
        public int IdDoctor { get; set; }
        [ForeignKey("IdDoctor")]
        public Doctor Doctor { get; set; }

        public IEnumerable<Prescription_Medicament> Prescription_Medicaments { get; set; } = new HashSet<Prescription_Medicament>();
   }
}
