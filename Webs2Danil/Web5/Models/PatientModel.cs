using System.ComponentModel.DataAnnotations;

namespace Web5.Models
{
    public class PatientModel
    {
        public int Id { get; set; }

        [Required]
        public string? name { get; set; }
        [Required]
        public string? hospitalAddress { get; set; }
        [Required]
        public string? phones { get; set; }
        [Required]
        public string? healingDoctors { get; set; }
    }
}
