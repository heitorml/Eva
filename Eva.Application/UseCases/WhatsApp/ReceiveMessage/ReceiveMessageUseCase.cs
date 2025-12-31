using Azure.AI.OpenAI;
using Eva.Application.Agents;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eva.Application.UseCases.WhatsApp.ReceiveMessage
{
    public class ReceiveMessageUseCase(
        AzureOpenAIClient azureOpenAIClient,
        IServiceScopeFactory serviceScopeFactory,
        IConfiguration configuration) : IReceiveMessageUseCase
    {
        public async Task<object> ReceiveMessageAsync(string userMessage)
        {
            try
            {
                var agentEva = EvaAgent.AddAgentEva(azureOpenAIClient, serviceScopeFactory, configuration);
                var response = await agentEva.RunAsync(userMessage);

                var result = response.Messages
                    .Where(c => c.Role == ChatRole.Assistant && !string.IsNullOrEmpty(c.Text))
                    .Select(x => x.Contents);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
         
        }
    }
}
