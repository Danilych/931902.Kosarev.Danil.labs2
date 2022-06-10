using System.ComponentModel.DataAnnotations;

namespace Web4.Models
{
    public class CodeModel
    {
        [Required]
        
        public string code { get; set; }



        public CodeModel()
        {
            code = "";
        }
    }
}
