using Azure;
using Azure.AI.OpenAI;
using Eva.Application.UseCases.WhatsApp.ReceiveMessage;
using Eva.Application.UseCases.WhatsApp.SendMessage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Eva.Application
{
    public static class Setup
    {
        public static void AddApplicationServices(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            var azureOpenAIChatClient = new AzureOpenAIClient(
                 new Uri(configuration["AzureOpenAI:Endpoint"]),
                 new AzureKeyCredential(configuration["AzureOpenAI:ApiKey"]));

            services.AddSingleton(azureOpenAIChatClient);
            services.AddScoped<IReceiveMessageUseCase, ReceiveMessageUseCase>();
            services.AddScoped<ISendMessageUseCase, SendMessageUseCase>();

        }

    }
}
