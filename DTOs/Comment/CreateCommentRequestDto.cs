using System.ComponentModel.DataAnnotations;

namespace FinShark.DTOs.Comment
{
    public class CreateCommentRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title must be atleast 3 characters")]
        [MaxLength(255, ErrorMessage = "Title exceeds 255 limit")]
        public required string Title { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Content must be atleast 3 characters")]
        [MaxLength(255, ErrorMessage = "Content exceeds 255 limit")]
        public required string Content { get; set; }
        public int StockId { get; set; }
    }
}