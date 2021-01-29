using AutoMapper;
using Imagizer.BLL.Models.Images;
using Imagizer.DAL.UnitOfWork.Repositories;
using System.Threading.Tasks;

namespace Imagizer.BLL.Services
{
	public class ImageService : IImageService
	{
		private readonly IMapper _mapper;
		private readonly IImageRepository _imageRepository;

		public ImageService(IMapper mapper, IImageRepository imageRepository)
		{
			_mapper = mapper;
			_imageRepository = imageRepository;
		}

		public async Task<Image> AddImageAsync(AddImageData data)
		{
			var dalData = _mapper.Map<DAL.Models.AddImageData>(data);

			var dalResult = await _imageRepository.AddAsync(dalData);

			return _mapper.Map<Image>(dalResult);
		}

		public async Task<DeleteErrorCode> DeleteImage(string name)
		{
			var dalError = await _imageRepository.DeleteAsync(name);

			return _mapper.Map<DeleteErrorCode>(dalError);
		}

		public async Task<Image> GetImageAsync(string name)
		{
			var dalResult = await _imageRepository.GetAsync(name);

			return _mapper.Map<Image>(dalResult);
		}
	}
}
