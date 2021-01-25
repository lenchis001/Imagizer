using Imagizer.DAL.Models;
using System.Threading.Tasks;

namespace Imagizer.DAL.UnitOfWork.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User> GetByCredsAsync(string email, string passwordHash = null);
	}
}
