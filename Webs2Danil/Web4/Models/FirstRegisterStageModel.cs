using System.ComponentModel.DataAnnotations;

namespace Web4.Models
{
    public class FirstRegisterStageModel
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string gender { get; set; }

        [Required]
        public string birthdayDay { get; set; }
        [Required]
        public string birthdayMonth { get; set; }
        [Required]
        public string birthdayYear { get; set; }

        public FirstRegisterStageModel()
        {
            birthdayDay = "";
            birthdayMonth = "";
            birthdayYear = "";
            firstName = "";
            lastName = "";
            gender = "";
        }
    }
}
