using Imagizer.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imagizer.DAL.UnitOfWork.Repositories
{
	public interface IImageRepository
	{
		Task<ICollection<Image>> GetAllAsync();

		Task<DeleteErrorCode> DeleteAsync(Image image);

		Task<Image> AddAsync(AddImageData data);
	}
}
