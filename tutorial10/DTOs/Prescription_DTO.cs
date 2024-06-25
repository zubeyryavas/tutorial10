
namespace tutorial10.DTOs
{
    public class Prescription_DTO
    {
        public string PatientFirstname { get; set; }
        public string patientLastName { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastname { get; set; }
        public List<Medicament_DTO> medicaments { get; set; }

    }
}
