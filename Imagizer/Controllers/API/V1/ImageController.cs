using AutoMapper;
using Imagizer.BLL.Services;
using Imagizer.Models.API.V1.Images;
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
		private readonly IImageService _imageService;
		private readonly IFileMetadataService _fileMetadataService;
		private readonly IMapper _mapper;

		public ImageController(
			ILogger<ImageController> logger,
			IImageService imageService,
			IFileMetadataService fileMetadataService,
			IMapper mapper)
		{
			_logger = logger;
			_imageService = imageService;
			_fileMetadataService = fileMetadataService;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<ActionResult<Image>> Post()
		{
			using (var stream = HttpContext.Request.BodyReader.AsStream())
			{
				using (var memoryStream = new MemoryStream())
				{
					await stream.CopyToAsync(memoryStream);

					var extention = _fileMetadataService.GetExtentionByContentType(Request.ContentType);

					var addImageData = new AddImageData
					{
						Extention = extention,
						Data = memoryStream.ToArray()
					};

					var bllAddImageData = _mapper.Map<BLL.Models.Images.AddImageData>(addImageData);
					var bllResult = await _imageService.AddImageAsync(bllAddImageData);

					var result = _mapper.Map<Image>(bllResult);

					return result;
				}
			}
		}

		[HttpGet("{name}")]
		public async Task<ActionResult<Image>> Get(string name)
		{
			var bllResult = await _imageService.GetImageAsync(name);
			var result = _mapper.Map<Image>(bllResult);

			var contentType = _fileMetadataService.GetContentTypeByExtention(Path.GetExtension(result.Name));

			if (contentType == null)
			{
				return this.Problem("An unknown file extention.");
			}
			else
			{
				if (result.Data.Length == 0)
				{
					return NotFound();
				}
				else
				{
					return File(result.Data, contentType);
				}
			}
		}

		[HttpDelete("{name}")]
		public async Task<ActionResult> Delete(string name)
		{
			var bllResult = await _imageService.DeleteImage(name);
			var result = _mapper.Map<DeleteErrorCode>(bllResult);

			switch (result)
			{
				case DeleteErrorCode.OK:
					return Ok();
				case DeleteErrorCode.NOT_FOUND:
					return NotFound();
				case DeleteErrorCode.UNKNOWN:
				default:
					return Problem();
			}
		}
	}
}
