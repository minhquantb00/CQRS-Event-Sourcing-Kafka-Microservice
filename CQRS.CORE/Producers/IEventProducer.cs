using CQRS.CORE.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.CORE.Producers
{
    public interface IEventProducer
    {
        Task ProducerAsync<T>(string topic, T @event) where T : BaseEvent;

    }
}
