using POST.QUERY.DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POST.QUERY.DOMAIN.IRepositories
{
    public interface ICommentRepository
    {
        Task CreateAsync(CommentEntity comment);
        Task UpdateAsync(CommentEntity comment);
        Task DeleteAsync(Guid commentId);
        Task<CommentEntity> GetCommentById(Guid commentId);
    }
}
