using Imagizer.DAL.Models;
using System.Threading.Tasks;

namespace Imagizer.DAL.UnitOfWork.Repositories
{
	public interface IImageRepository
	{
		Task<Image> GetAsync(string name);

		Task<DeleteErrorCode> DeleteAsync(string name);

		Task<Image> AddAsync(AddImageData data);
	}
}
