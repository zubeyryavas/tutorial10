using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tutorial10.DTOs;
using tutorial10.Models;

namespace tutorial10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private S20697Context _context;

        public HospitalController(S20697Context context) => _context = context;

        [HttpGet("/prescriptions/{idPrescription}")]
        public async Task<IActionResult> GetdataAsync([FromRoute]int idPrescription)
        {

            var checkPrescription = _context.Prescriptions.Where(p => p.Idprescription == idPrescription).Any();
            if(!checkPrescription) return NotFound("Prescription Doesn't exists!!!");

            var result = await _context.Prescriptions.Where(x => x.Idprescription == idPrescription)
                .Select(x => new Prescription_DTO
                {
                    DoctorFirstName = x.Doctor.FirstName,
                    DoctorLastname = x.Doctor.LastName,
                    PatientFirstname = x.Patient.FirstName,
                    patientLastName = x.Patient.LastName,
                    medicaments = (List<Medicament_DTO>)x.Prescription_Medicaments.Select( y => new Medicament_DTO
                    {
                            Name = y.Medicament.name,
                            Type = y.Medicament.Type,
                            Description = y.Medicament.Description,
                    })      
                }).ToListAsync();
            return Ok(result);
        }


    }
    
}
