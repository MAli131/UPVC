using System.ComponentModel.DataAnnotations;

namespace UPVC.Models
{
    public class ChatbotFAQ : BaseEntity
    {
        [Required]
        [StringLength(500)]
        public string QuestionAr { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string QuestionEn { get; set; } = string.Empty;

        [Required]
        public string AnswerAr { get; set; } = string.Empty;

        [Required]
        public string AnswerEn { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(100)]
        public string? Category { get; set; }
    }
}
