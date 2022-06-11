using System.ComponentModel.DataAnnotations;

namespace Web6.Models
{
    public class Topic
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        public string? TopicName { get; set; }

        public string? TopicCreator { get; set; }
        public string? TopicCreationDate { get; set; }
        public int RepliesAmount { get; set; }
        public string? LasReplyCreator { get; set; }
        public string? LastReplyDate { get; set; }

        //Forum data
        public int? ForumCategoryId { get; set; }
        public ForumCategory? ForumCategory { get; set; }
    }
}
