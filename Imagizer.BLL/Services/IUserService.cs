using Imagizer.BLL.Models.Entities.Users;
using System.Threading.Tasks;

namespace Imagizer.BLL.Services
{
	public interface IUserService : ICrudService<User, AddUserData>
	{
		Task<User> GetByCredsAsync(string email, string passwordHash);
	}
}
