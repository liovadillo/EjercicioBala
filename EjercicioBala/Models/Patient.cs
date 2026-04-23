using System.ComponentModel.DataAnnotations;

namespace EjercicioBala.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, 120)]
        public int Age { get; set; }
        public string Diagnosis { get; set; }
        public bool IsActive { get; set; }

    }
}
