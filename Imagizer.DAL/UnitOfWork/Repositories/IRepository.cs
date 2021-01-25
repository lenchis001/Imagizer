using Imagizer.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imagizer.DAL.UnitOfWork.Repositories
{
	public interface IRepository<T> where T : IdAware
	{
		Task<ICollection<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task<T> UpdateAsync(T entity);
		Task DeteleAsync(int id);
		Task<T> AddAsync(T entity);
	}
}
