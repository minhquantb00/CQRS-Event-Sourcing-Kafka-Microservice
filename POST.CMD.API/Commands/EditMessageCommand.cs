using CQRS.CORE.Commands;

namespace POST.CMD.API.Commands
{
    public class EditMessageCommand : BaseCommand
    {
        public string Message { get; set; }
    }
}
