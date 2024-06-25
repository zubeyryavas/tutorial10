using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tutorial10.Models;

namespace tutorial10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        private S20697Context _context;

        public DoctorController(S20697Context context) => _context = context;

        [HttpGet("/doctors")]
        [Authorize]
        public async Task<IActionResult> GetDoctorAsync()
        {
            var result = await _context.Doctors.Select(x => new
            {
                IdDoctor = x.IdDoctor,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            }).ToListAsync();

            return Ok(result);

        }

        [HttpGet("/doctors/{id}")]
        [Authorize]
        public async Task<IActionResult> GetDoctor([FromRoute] int idDoctor)
        {
            var result = _context.Doctors.Select(x => new Doctor
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                IdDoctor = x.IdDoctor,
                Email = x.Email
            }).Where(x => x.IdDoctor == idDoctor).ToList();
            return Ok(result);
        }


        [HttpPost("/doctors")]
        [Authorize]
        public async Task<IActionResult> PostDoctorAsync([FromBody]Doctor doc)
        {
            Doctor newDoctor = new Doctor
            {
                IdDoctor = doc.IdDoctor,
                FirstName=doc.FirstName,
                LastName=doc.LastName,
                Email=doc.Email
            };

             _context.Doctors.Add(newDoctor);
            await _context.SaveChangesAsync();

            return Ok("Doctor added !");
        }

        [HttpDelete("/doctors/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDoctorAsync([FromRoute]int id)
        {
            var result= await _context.Doctors.Where(x => x.IdDoctor == id).FirstAsync();
            _context.Doctors.Remove((Doctor)result);
            await _context.SaveChangesAsync();

            return Ok("Doctor has been deleted !");
        }

        [HttpPut("/docors/{id}")]
        [Authorize]
        public async Task<IActionResult> PutDoctorAsync([FromBody]Doctor doc, [FromRoute]int id)
        {
            var result = await _context.Doctors.FirstAsync(x => x.IdDoctor == id);
            result.FirstName = doc.FirstName;
            result.LastName = doc.LastName;
            result.Email = doc.Email;

            await _context.SaveChangesAsync();

            return Ok("Doctor updated !");

        }
    }
}
