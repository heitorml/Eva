using Eva.Application.Events;
using Eva.Application.UseCases.WhatsApp.ReceiveMessage;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Eva.WhatsApp.Controllers.v1
{
    [Route("api/whatsapp")]
    [ApiController]
    public class EvaWhatsAppController(IReceiveMessageUseCase useCase, IBus bus) : ControllerBase
    {
        [HttpPost("webhook")]
        public async Task<IActionResult> Post(string userMessage) => Ok(await useCase.ReceiveMessageAsync(userMessage));

        [HttpPost("webhook/v2")]
        public async Task<IActionResult> PostAsync(string userMessage) 
        {
            await bus.Publish(new ConversationStarted(Guid.NewGuid().ToString(), userMessage));
            return Ok(); 
        }
    }
}
