using System.ComponentModel.DataAnnotations;

namespace Web6.Models
{
    public class ForumCategory
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        public string? CategoryName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        public string? CategoryDescription { get; set; }
    }
}
