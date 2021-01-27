using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Imagizer.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ImageController : ControllerBase
	{
		private readonly ILogger<ImageController> _logger;

		public ImageController(ILogger<ImageController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public async Task<ActionResult> Post()
		{
			using (var stream = HttpContext.Request.BodyReader.AsStream())
			{
				using (var memoryStream = new MemoryStream())
				{
					await stream.CopyToAsync(memoryStream);
					await System.IO.File.WriteAllBytesAsync("D:/Temp/test.jpg", memoryStream.ToArray());
				}
			}

			return Ok();
		}
	}
}
