using CQRS.CORE.Commands;

namespace POST.CMD.API.Commands
{
    public class NewPostCommand : BaseCommand
    {
        public string Author { get; set; }
        public string Message { get; set; }
    }
}
