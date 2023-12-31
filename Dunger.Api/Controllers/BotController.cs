﻿using Dunger.Application.Services.TelegramServices.TelegramBotServices;
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
            try
            {
                await handleUpdateService.HandleUpdateAsync(update, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
