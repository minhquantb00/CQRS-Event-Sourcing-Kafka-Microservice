using POST.COMMONS.Events;
using POST.QUERY.DOMAIN.Entities;
using POST.QUERY.DOMAIN.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POST.QUERY.INFRASTRUCTURE.Handlers
{
    public class EventHandler : IEventHandler
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        public EventHandler(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task On(PostCreatedEvent @event)
        {
            var post = new PostEntity
            {
                Id = @event.Id,
                Author = @event.Author,
                DataPosted = @event.DatePosted,
                Message = @event.Message,
            };
            await _postRepository.CreateAsync(post);
        }

        public async Task On(MessageUpdatedEvent @event)
        {
            var post = await _postRepository.GetByIdAsync(@event.Id);
            if (post == null) return;
            post.Message = @event.Message;
            await _postRepository.UpdateAsync(post);
        }

        public async Task On(PostLikeEvent @event)
        {
            var post = await _postRepository.GetByIdAsync(@event.Id);
            if (post == null) return;
            post.Likes++;
            await _postRepository.UpdateAsync(post);
        }

        public async Task On(CommentAddedEvent @event)
        {
            var comment = new CommentEntity
            {
                PostId = @event.Id,
                Id = @event.CommentId,
                CommentDate = @event.CommentDate,
                Comment = @event.Comment,
                Username = @event.Username,
                Edited = false
            };
            await _commentRepository.CreateAsync(comment);
        }

        public async Task On(CommentUpdatedEvent @event)
        {
            var comment = await _commentRepository.GetCommentById(@event.Id);
            if (comment == null) return;
            comment.Comment = @event.Comment;
            comment.Edited = true;
            comment.CommentDate = (DateTime) @event.UpdateTime;
            await _commentRepository.UpdateAsync(comment);
        }

        public async Task On(CommentRemovedEvent @event)
        {
            var comment = await _commentRepository.GetCommentById(@event.Id);
            if (comment == null) return;
            await _commentRepository.DeleteAsync(comment.Id);
        }

        public async Task On(PostRemovedEvent @event)
        {
            var post = await _postRepository.GetByIdAsync(@event.Id);
            if (post == null) return;
            await _postRepository.DeleteAsync(post.Id);
        }
    }
}
