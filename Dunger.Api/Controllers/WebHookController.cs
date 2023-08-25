using Dunger.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Dunger.Api.Controllers
{
    public class WebHookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] UpdateHandlerService service, [FromBody] Update update)
        {
            await service.EchoAsync(update);

            return Ok();
        }
    }
}
