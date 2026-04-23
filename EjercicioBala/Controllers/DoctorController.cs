using EjercicioBala.Models;
using EjercicioBala.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioBala.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> Get() {

            return Ok(_doctorService.GetAll());

        }

        [HttpGet("{id}")]
        public ActionResult<Doctor> GetById(int id)
        {
            var doctor = _doctorService.GetById(id);
            if (doctor == null)
                return NotFound($"Doctor ID: {id} doesnt exists");

            return Ok(doctor);
        }
        [HttpGet("available")]
        public ActionResult<IEnumerable<Doctor>> GetAvailable()
        {

            return Ok(_doctorService.GetAllAvailable());

        }
        [HttpGet("search")]
        public ActionResult<IEnumerable<Doctor>> GetBySpeciality([FromQuery] string speciality)
        {
            if (string.IsNullOrEmpty(speciality))
                return BadRequest();

            return Ok(_doctorService.GetAllSpeciality(speciality));
        }

        [HttpPost]
        public ActionResult<Doctor> Post(Doctor obj) {

            if (!ModelState.IsValid)
                return BadRequest("Invalid Doctor object");

            var doctor = _doctorService.Insert(obj);
            return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, doctor);
        }

        [HttpPut("{id}")]
        public ActionResult<Doctor> Put([FromRoute] int id, [FromBody] Doctor obj)
        {
            if (id != obj.Id)
                return BadRequest("Ids doesn't match.");

            var doctor = _doctorService.Update(id, obj);
            if (doctor == null)
                return NotFound($"Doctor ID {id} not found. No Update");

            return Ok(doctor);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var wasDeleted = _doctorService.Delete(id);
            if (!wasDeleted)
                return NotFound($"Doctor ID {id} not found. No Delete");

            return NoContent();

        }
    }
}
