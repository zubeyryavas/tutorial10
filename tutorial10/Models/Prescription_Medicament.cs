using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tutorial10.Models;

namespace tutorial10.Models
{
    [Table("Prescription_Medicament")]
    public class Prescription_Medicament
    {
        [Key]
        public int IdMedicament { get; set; }
        [ForeignKey("IdMedicament")]
        public Medicament Medicament { get; set; }
        public int IdPrescription { get; set; }
        [ForeignKey("IdPrescription")]
        public Prescription Prescription { get; set; }
        public int Dose { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Max Length is 100")]
        public string Details { get; set; }
    }
}
