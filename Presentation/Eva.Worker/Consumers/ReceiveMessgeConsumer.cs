using Eva.Application.Events;
using Eva.Application.UseCases.WhatsApp.ReceiveMessage;
using MassTransit;

namespace Eva.Worker.Consumers
{
    public class ReceiveMessgeConsumer
        (IReceiveMessageUseCase usecase, ILogger<ReceiveMessgeConsumer> logger) : IConsumer<ConversationStarted>
    {
        public async Task Consume(ConsumeContext<ConversationStarted> context)
        {
            var result = await usecase.ReceiveMessageAsync(context.Message.Message);
            logger.LogInformation("Received message: {Message}", context.Message.Message);
            logger.LogTrace("Received message: {Message}", context.Message.Message);
            await Task.CompletedTask;
        }
    }
}
