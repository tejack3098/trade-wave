using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllAsync();

        public Task<Comment?> GetByIdAsync(int id);

        public Task<Comment> CreateAsync(Comment comment);

        public Task<Comment?> UpdateAsync(int id, Comment comment);
    }

}