using System.ComponentModel.DataAnnotations;

namespace Web4.Models
{
    public class SecondRegisterStageModel
    {
        [Required]
        public string email { get; set; }
        [Required]
        [StringLength(255,ErrorMessage="Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage ="Passwords should be same")]
        public string confirmPassword { get; set; }
        [Required]
        public string remember { get; set; }
        public SecondRegisterStageModel()
        {
            email = "";
            password = "";
            confirmPassword = "";
            remember = "";
        }
    }
}
