using Cqrs.Domain.Exceptions;
using CQRS.CORE.Domain;
using CQRS.CORE.Events;
using CQRS.CORE.Exceptions;
using CQRS.CORE.Infrastructure;
using CQRS.CORE.Producers;
using POST.CMD.DOMAIN.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POST.CMD.INFRASTRUCTURE.Stores
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IEventProducer _eventProducer;
        public EventStore(IEventStoreRepository eventStoreRepository, IEventProducer eventProducer)
        {
            _eventStoreRepository = eventStoreRepository;
            _eventProducer = eventProducer;
        }

        public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);
            if(eventStream == null || eventStream.Any())
            {
                throw new AggregateNotFoundException("Incorrect post ID provided!");
            }
            return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
        }

        public async Task SaveEventAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);
            if(expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            {
                throw new ConcurrencyExceptions();
            }
            var version = expectedVersion;
            foreach(var @event in events)
            {
                version++;
                @event.Version = version;
                var eventType = @event.GetType().Name;
                var eventModel = new EventModel
                {
                    Timestamp = DateTime.Now,
                    AggregateIdentifier = aggregateId,
                    AggregateType = nameof(PostAggregate),
                    Version = version,
                    EventType = eventType,
                    EventData = @event
                };
                await _eventStoreRepository.SaveAsync(eventModel);

                var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
                await _eventProducer.ProducerAsync(topic, @event);
            }
        }
    }
}
