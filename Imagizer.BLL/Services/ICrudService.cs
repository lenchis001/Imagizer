using Imagizer.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imagizer.BLL.Services
{
	public interface ICrudService<T, ADD_TYPE>
	{
		Task<DefaultDataFetchResult<ICollection<T>>> GetAllAsync();
		Task<DefaultDataFetchResult<T>> GetByIdAsync(int id);
		Task<DefaultDataFetchResult<T>> AddAsync(ADD_TYPE entity);
		Task<DefaultFetchResult> DeleteAsync(int id);
		Task<DefaultDataFetchResult<T>> UpdateAsync(T entity);
	}
}
