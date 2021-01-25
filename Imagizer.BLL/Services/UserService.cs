using AutoMapper;
using Imagizer.BLL.Models;
using Imagizer.BLL.Models.Entities.Users;
using Imagizer.DAL.UnitOfWork.Repositories;
using System.Threading.Tasks;

namespace Imagizer.BLL.Services
{
	class UserService : CrudService<User, AddUserData, IUserRepository, DAL.Models.User>, IUserService
	{
		public UserService(IMapper mapper, IUserRepository repository) : base(mapper, repository)
		{
		}

		public new async Task<DefaultDataFetchResult<User>> AddAsync(AddUserData entity)
		{
			if (await GetByCredsAsync(entity.Email) != null)
			{
				return new DefaultDataFetchResult<User>
				{
					Error = ErrorCode.ACCESS_DENIED
				};
			}

			return await base.AddAsync(entity);
		}

		public async Task<User> GetByCredsAsync(string email, string passwordHash = null)
		{
			var dalResult = await _repository.GetByCredsAsync(email, passwordHash);
			return _mapper.Map<User>(dalResult);
		}
	}
}
