using System.ComponentModel.DataAnnotations;

namespace FinShark.DTOs.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title must be atleast 3 characters")]
        [MaxLength(255, ErrorMessage = "Title exceeds 255 limit")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(3, ErrorMessage = "Content must be atleast 3 characters")]
        [MaxLength(255, ErrorMessage = "Content exceeds 255 limit")]
        public string Content { get; set; } = string.Empty;
    }
}