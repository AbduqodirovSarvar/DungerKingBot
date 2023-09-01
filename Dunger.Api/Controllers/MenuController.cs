using Dunger.Application.UseCases.Menus.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dunger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;
        public MenuController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateMenuCommand command, IFormFile image)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (image is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            string webRootPath = _env.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string filePath = Path.Combine(webRootPath, "Images", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            command.PhotoPath = filePath;
            command.Title = fileName;
            
            return Ok(await _mediator.Send(command));
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromForm] UpdateMenuCommand command, IFormFile image)
        {
            return Ok();
        }
    }
}
