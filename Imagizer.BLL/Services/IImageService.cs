using Imagizer.BLL.Models.Images;
using System.Threading.Tasks;

namespace Imagizer.BLL.Services
{
	public interface IImageService
	{
		Task<Image> AddImageAsync(AddImageData data);

		Task<Image> GetImageAsync(string name);

		Task<DeleteErrorCode> DeleteImage(string name);
	}
}
