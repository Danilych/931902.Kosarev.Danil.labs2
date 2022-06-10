using System.ComponentModel.DataAnnotations;

namespace Web5.Models
{
    public class HospitalModel
    {
        public int Id { get; set; }

        [Required]
        public string? name { get; set; }
        [Required]
        public string? address { get; set; }
        [Required]
        public string? phones { get; set; }
    }
}
