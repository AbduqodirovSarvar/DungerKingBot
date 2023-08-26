using Dunger.Application.Services.TelegramBotServices;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Dunger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update, [FromServices] UpdateHandlerService handleUpdateService, CancellationToken cancellationToken)
        {
            await handleUpdateService.HandleUpdateAsync(update, cancellationToken);
            return Ok();
        }
    }
}
