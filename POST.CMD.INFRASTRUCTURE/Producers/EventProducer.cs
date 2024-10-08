﻿using Confluent.Kafka;
using CQRS.CORE.Events;
using CQRS.CORE.Producers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace POST.CMD.INFRASTRUCTURE.Producers
{
    public class EventProducer : IEventProducer
    {
        private readonly ProducerConfig _config;
        public EventProducer(IOptions<ProducerConfig> config)
        {
            _config = config.Value;
        }
        public async Task ProducerAsync<T>(string topic, T @event) where T : BaseEvent
        {
            using var producer = new ProducerBuilder<string, string>(_config)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8)
                .Build();

            var eventMessage = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonSerializer.Serialize(@event, @event.GetType())
            };

            try
            {
                var deliveryResult = await producer.ProduceAsync(topic, eventMessage);

                if (deliveryResult.Status == PersistenceStatus.NotPersisted)
                {
                    throw new Exception($"Could not produce {@event.GetType().Name} message to topic - {topic} due to the following reason: {deliveryResult.Message}.");
                }

                Console.WriteLine($"Message produced to topic: {topic} | Partition: {deliveryResult.Partition} | Offset: {deliveryResult.Offset}");
            }
            catch (ProduceException<string, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }

    }
}
