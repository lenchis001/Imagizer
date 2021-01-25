using Imagizer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Imagizer.DAL.UnitOfWork.Repositories
{
	class UserRepository : EfRepository<User>, IUserRepository
	{
		public UserRepository(DbSet<User> dbSet, Func<Task> saveChangesDelegate, SemaphoreSlim semaphore) : base(dbSet, saveChangesDelegate, semaphore)
		{
		}

		public async Task<User> GetByCredsAsync(string email, string passwordHash = null)
		{
			User result = null;

			await _semaphore.WaitAsync();
			try
			{
				result = await _dbSet.FirstOrDefaultAsync(u => u.Email == email && passwordHash == null ? true : u.PasswordHash == passwordHash);

			}
			finally
			{
				_semaphore.Release();
			}

			return result;
		}
	}
}
