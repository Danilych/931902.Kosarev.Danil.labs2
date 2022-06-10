using System.ComponentModel.DataAnnotations;

namespace Web5.Models
{
    public class DoctorModel
    {
        public int Id { get; set; }

        [Required]
        public string? name { get; set; }
        [Required]
        public string? workAdress { get; set; }
        [Required]
        public string? phones { get; set; }
    }
}
