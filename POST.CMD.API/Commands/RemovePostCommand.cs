using CQRS.CORE.Commands;

namespace POST.CMD.API.Commands
{
    public class RemovePostCommand : BaseCommand
    {
        public string Username { get; set; }
    }
}
