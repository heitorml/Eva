using MassTransit;
using System.Diagnostics.CodeAnalysis;

namespace Eva.WhatsApp.Configurations
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
                x.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(serviceBus);
                    cfg.UseServiceBusMessageScheduler();
                    cfg.ConfigureEndpoints(context);
                });
            });

        }
    }
}
