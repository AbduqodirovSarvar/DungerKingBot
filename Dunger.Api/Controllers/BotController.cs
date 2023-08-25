using Dunger.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Dunger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        public BotController() { }
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] UpdateHandlerService service, [FromBody] Update update)
        {
            await service.EchoAsync(update);

            return Ok();
        }
    }
}
