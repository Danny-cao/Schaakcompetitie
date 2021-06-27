using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client.Exceptions;
using Roger.MicroServices.Events;
using Roger.MicroServices.Host;
using Roger.RabbitMQBus;

namespace Schaakcompetitie.Frontend
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void UseRoger(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("RabbitMQ");
            
            var eventbus = configuration.GetSection("Eventbus");

            string exchange = eventbus.GetValue<string>("Exchange");
            string queue = eventbus.GetValue<string>("Queue");

            var contextBuilder = new RabbitMQBusContextBuilder()
                .WithConnectionString(connectionString)
                .WithExchange(exchange);
            
            var context = Policy.Handle<BrokerUnreachableException>()
                .WaitAndRetryForever(sleepDurationProvider => TimeSpan.FromSeconds(5))
                .Execute(contextBuilder.CreateContext);
            
            var rogerLoggerFactory = LoggerFactory.Create(configure =>
            {
                configure.AddConsole().SetMinimumLevel(LogLevel.Trace)
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Error);
            });
            
            services.AddTransient<IEventSender, EventSender>();

            var host = new MicroserviceHostBuilder()
                .SetLoggerFactory(rogerLoggerFactory)
                .RegisterDependencies(services)
                .WithQueueName(queue)
                .UseConventions()
                .WithBusContext(context)
                .CreateHost();
            
            services.AddSingleton(context);
            services.AddSingleton(host);
            
            services.AddHostedService<RogerHost>();
        }
    }
}