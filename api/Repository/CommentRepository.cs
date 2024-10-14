using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository: ICommentRepository
    {

        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync() 
        { 
            return await _context.Comments
                        .Include(c => c.AppUser)
                        .ToListAsync();
        }

        public Task<Comment?> GetByIdAsync(int id)
        {
            return _context.Comments
                        .Include(c => c.AppUser)
                        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }


        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = comment.Title;
            existingComment.Content = comment.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            
            if (comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}