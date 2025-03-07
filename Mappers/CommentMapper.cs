using FinShark.DTOs.Comment;
using FinShark.Models;
namespace FinShark.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
                CreatedBy = comment.AppUser.UserName!
            };
        }

        public static Comment ToComment(this CreateCommentRequestDto createCommentRequestDto, AppUser user)
        {
            return new Comment
            {
                Title = createCommentRequestDto.Title,
                Content = createCommentRequestDto.Content,
                StockId = createCommentRequestDto.StockId,
                AppUserId = user.Id
            };
        }
    }
}