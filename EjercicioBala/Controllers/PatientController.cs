using EjercicioBala.DTO;
using EjercicioBala.Models;
using EjercicioBala.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioBala.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Patient>> Get()
        {

            return Ok(_patientService.GetAll());
        }

        [HttpGet("active")]
        public ActionResult<IEnumerable<Patient>> GetAllActive()
        {
            var activePatients = _patientService.GetAllActive();
            return Ok(activePatients);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Patient>> GetByDiagnosis([FromQuery] string diagnosis)
        {
            if (string.IsNullOrEmpty(diagnosis))
                return BadRequest();
            
            var diagnosedPatients = _patientService.GetByDiagnosis(diagnosis);            
            
            return Ok(diagnosedPatients);
        }

        [HttpGet("{id}")]
        public ActionResult<Patient> GetById([FromRoute] int id)
        {
            var patient = _patientService.GetById(id);

            if (patient == null)
                return NotFound($"Record ID: {id} doesnt exists");

            return Ok(patient);

        }

        [HttpPost]
        public ActionResult<Patient> Post([FromBody] Patient obj)
        {
            if (ModelState.IsValid)
            {
                var patient = _patientService.Insert(obj);
                return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
            }
            else
                return BadRequest("Invalid Patient object");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {         
            if (!_patientService.Delete(id))
                return NotFound();
            
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<Patient> Put(int id, [FromBody] Patient obj)
        {
            if (id != obj.Id)
                return BadRequest();

            var patient = _patientService.Update(id, obj);
          
            if(patient == null)
                return NotFound();

            return Ok(patient);
        }

        [HttpPatch("{id}/status")]
        public ActionResult<Patient> Patch([FromRoute]int id, [FromBody] StatusUpdateDto obj)
        {
          
            var patient = _patientService.UpdateStatus(id, obj);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

    }
}
