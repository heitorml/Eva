using Eva.Worker.Consumers;
using MassTransit;
using System.Diagnostics.CodeAnalysis;

namespace Eva.Worker.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class ServiceBus
    {
        public static void AddLogisticServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceBus = configuration.GetConnectionString("ServiceBus");

            services.AddMassTransit(x =>
            {
                x.AddServiceBusMessageScheduler();
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<ReceiveMessgeConsumer>();

                x.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(serviceBus);

                    cfg.UseServiceBusMessageScheduler();

                    cfg.ReceiveEndpoint("conversation-started", e =>
                    {
                        e.ConfigureDeadLetterQueueDeadLetterTransport();
                        e.ConfigureDeadLetterQueueErrorTransport();

                        e.UseKillSwitch(options => options
                         .SetActivationThreshold(10)
                         .SetTripThreshold(0.15)
                         .SetRestartTimeout(m: 1));

                        e.UseMessageRetry(r =>
                        {
                            r.Intervals(
                                TimeSpan.FromSeconds(5),
                                TimeSpan.FromSeconds(8),
                                TimeSpan.FromSeconds(10));
                        });

                        e.UseInMemoryOutbox(context);

                        e.ConfigureConsumer<ReceiveMessgeConsumer>(context);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

        }
    }
}
