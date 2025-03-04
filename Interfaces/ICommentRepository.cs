using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinShark.DTOs.Comment;
using FinShark.Models;

namespace FinShark.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync(int id);

        Task<Comment> CreateAsync(CreateCommentRequestDto comment, AppUser appUser);

        Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateCommentRequestDto);

        Task<Comment?> DeleteAsync(int id);
    }
}