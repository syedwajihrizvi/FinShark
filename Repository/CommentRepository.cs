using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinShark.Data;
using FinShark.DTOs.Comment;
using FinShark.Interfaces;
using FinShark.Mappers;
using FinShark.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;

        }

        public async Task<Comment> CreateAsync(CreateCommentRequestDto commentDto, AppUser appUser)
        {

            var commentModel = commentDto.ToComment(appUser);
            await _context.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(c => c.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.Include(c => c.AppUser).FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
                return null;
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateCommentRequestDto)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return null;
            comment.Title = updateCommentRequestDto.Title;
            comment.Content = updateCommentRequestDto.Content;
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return null;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}