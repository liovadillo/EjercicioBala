using Microsoft.AspNetCore.Mvc;

namespace EjercicioBala.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private static List<WeatherForecast> addedRecords = new List<WeatherForecast>();

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("{id}")]
        public WeatherForecast Get([FromRoute] int id)
        {
            return new WeatherForecast
            {
                Id = id,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(id)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        [HttpPost]
        public IActionResult Post([FromBody] WeatherForecast obj)
        {
            if (!addedRecords.Any(r => r.Id == obj.Id))
            {
                addedRecords.Add(obj);
                return CreatedAtAction(nameof(Get), new { id = obj.Id }, obj);
            }
            else
            {
                return BadRequest("Id already exists");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var record = addedRecords.FirstOrDefault(r => r.Id == id);
            if (record != null)
            {
                addedRecords.Remove(record);
                return NoContent();
            }
            else
            {
                return NotFound($"Record ID: {id} doesnt exists");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] WeatherForecast obj)
        {
            if (id.Equals(obj.Id)) {
                var index = addedRecords.FindIndex(r => r.Id == obj.Id);
                if (index > -1)
                {
                    addedRecords[index].Summary = obj.Summary;
                    addedRecords[index].Date = obj.Date;
                    addedRecords[index].TemperatureC = obj.TemperatureC;

                    return Ok(addedRecords[index]);
                }
                else
                {
                    return NotFound($"Record ID: {obj.Id} doesnt exists");
                }
            }
            else
                return BadRequest($"Route ID: {id} and Object ID: {obj.Id}, doesn't match ");
        }
    }

}
