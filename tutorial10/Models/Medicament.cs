using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tutorial10.Models
{
    [Table("Medicament")]
    public class Medicament
    {
        [Key]
        public int IdMedicament { get; set; }

        [Required,MaxLength(100,ErrorMessage = "Not more than 100 letters")]
        public string name { get; set; }

        [Required, MaxLength(100,ErrorMessage = "Not more than 100 letters")]
        public string Description { get; set; }

        [Required, MaxLength(100,ErrorMessage = "Not more than 100 letters")]
        public string Type { get; set; }
        public IEnumerable<Prescription_Medicament> Prescription_Medicaments { get; set; } = new HashSet<Prescription_Medicament>();
    }
}
