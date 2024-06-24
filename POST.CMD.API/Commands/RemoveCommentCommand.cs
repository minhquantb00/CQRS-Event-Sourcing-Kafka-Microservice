﻿using CQRS.CORE.Commands;

namespace POST.CMD.API.Commands
{
    public class RemoveCommentCommand : BaseCommand
    {
        public Guid CommentId { get; set; }
        public string Username { get; set; }
    }
}
