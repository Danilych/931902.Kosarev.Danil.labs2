using System.ComponentModel.DataAnnotations;

namespace Web6.Models
{
    public class TopicPost
    {
        public int Id { get; set; }
        [Required]
        public string? TopicMessage1 { get; set; }
        public string? TopicMessage2 { get; set; }

        //Creation and edit data

        public string? TopicCreator { get; set; }
        public string? TopicCreationDate { get; set; }
        public string? TopicEditDate { get; set; }

        //Topic data

        public int? TopicId { get; set; }
        public Topic? Topic { get; set; }

        //Topic pictures (TODO)
    }
}
