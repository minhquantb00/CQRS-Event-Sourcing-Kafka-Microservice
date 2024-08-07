﻿using Amazon.Runtime.Internal.Util;
using CQRS.CORE.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POST.QUERY.INFRASTRUCTURE.Consumers
{
    public class ConsumerHostedService : IHostedService
    {
        private readonly ILogger<ConsumerHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;
        public ConsumerHostedService(IServiceProvider serviceProvider, ILogger<ConsumerHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event consumer service running");
            using(IServiceScope scope = _serviceProvider.CreateScope())
            {
                var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();
                var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
                Task.Run(() => eventConsumer.Consume(topic), cancellationToken);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event consumer service stopped.");
            return Task.CompletedTask;
        }
    }
}
