using CQRS.CORE.Commands;

namespace POST.CMD.API.Commands
{
    public class LikePostCommand : BaseCommand
    {
        public string Comment { get; set; }
        public string Username { get; set; }
    }
}
