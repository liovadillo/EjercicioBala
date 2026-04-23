using System.ComponentModel.DataAnnotations;

namespace EjercicioBala.DTO
{
    public class StatusUpdateDto
    {
        [Required]
        public bool IsActive { get; set; }
    }
}
