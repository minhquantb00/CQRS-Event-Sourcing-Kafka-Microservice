using Microsoft.EntityFrameworkCore;
using POST.QUERY.DOMAIN.Entities;
using POST.QUERY.DOMAIN.IRepositories;
using POST.QUERY.INFRASTRUCTURE.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POST.QUERY.INFRASTRUCTURE.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DatabaseContextFactory _contextFactory;
        public CommentRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task CreateAsync(CommentEntity comment)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Comments.Add(comment);
            _ = await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid commentId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            var comment = await GetCommentById(commentId);
            if(comment == null) return;
            context.Comments.Remove(comment);
            _ = await context.SaveChangesAsync();
        }

        public async Task<CommentEntity> GetCommentById(Guid commentId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Comments.Where(x => x.Id == commentId).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(CommentEntity comment)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            context.Comments.Update(comment);
            _ = await context.SaveChangesAsync();
        }
    }
}
