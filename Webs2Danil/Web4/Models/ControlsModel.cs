using System.ComponentModel.DataAnnotations;

namespace Web4.Models
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class ControlsModel
    {
        [Required]
        public string textBox { get; set; }
        [Required]
        public string textArea { get; set; }
        [Required]
        public string checkBox { get; set; }
        [Required]
        public string radio { get; set; }
        [Required]
        public string dropDownList {get;set;}
        [Required]
        public string listBox {get;set;}

        public ControlsModel()
        {
            textBox = "";
            textArea = "";
            checkBox = "";
            radio = "";
            dropDownList = "";
            listBox = "";
        }
    }
}
