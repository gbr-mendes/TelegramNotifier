using Microsoft.AspNetCore.Mvc;
using TelegramNotifier.Domain.Interfaces;

namespace TelegramNotifier.Controllers
{
    public class NotifierController : Controller
    {
        private readonly IMessageService _messageService;

        public NotifierController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("send-alert")]
        public IActionResult Notify(string message)
        {
            _messageService.SendMessage(message);
            return Ok(new { message = "Mensagem enviada com sucesso" });
        }
    }
}
