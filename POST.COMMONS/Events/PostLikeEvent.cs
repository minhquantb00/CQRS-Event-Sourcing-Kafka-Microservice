using CQRS.CORE.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POST.COMMONS.Events
{
    public class PostLikeEvent : BaseEvent
    {
        public PostLikeEvent() : base(nameof(PostLikeEvent)) { }    

    }
}
