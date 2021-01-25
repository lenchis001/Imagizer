using Imagizer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Imagizer.DAL.UnitOfWork.Repositories
{
	public class EfRepository<T> : IRepository<T> where T : IdAware
	{
		protected readonly DbSet<T> _dbSet;
		protected readonly Func<Task> _saveChangesDeleagte;
		protected readonly SemaphoreSlim _semaphore;

		public EfRepository(DbSet<T> dbSet, Func<Task> saveChangesDelegate, SemaphoreSlim semaphore)
		{
			_dbSet = dbSet;
			_saveChangesDeleagte = saveChangesDelegate;
			_semaphore = semaphore;
		}

		public async Task<T> AddAsync(T entity)
		{
			await _semaphore.WaitAsync();

			var result = await _dbSet.AddAsync(entity);
			await _saveChangesDeleagte();

			_semaphore.Release();

			return result.Entity;
		}

		public async Task DeteleAsync(int id)
		{
			await _semaphore.WaitAsync();

			T entity = await _dbSet
				.AsNoTracking()
				.FirstOrDefaultAsync(e => e.Id == id);

			if (entity == default)
			{
				_semaphore.Release();
				throw new KeyNotFoundException();
			}

			_dbSet.Remove(entity);
			await _saveChangesDeleagte();

			_semaphore.Release();
		}

		public async Task<ICollection<T>> GetAllAsync()
		{
			await _semaphore.WaitAsync();

			var result = await _dbSet
				.AsNoTracking()
				.ToArrayAsync();

			_semaphore.Release();

			return result;
		}

		public async Task<T> GetByIdAsync(int id)
		{
			await _semaphore.WaitAsync();

			var result = await _dbSet
				.AsNoTracking()
				.FirstOrDefaultAsync(e => e.Id == id);

			_semaphore.Release();

			return result;
		}

		public async Task<T> UpdateAsync(T entity)
		{
			await _semaphore.WaitAsync();

			var result = _dbSet.Update(entity);
			await _saveChangesDeleagte();

			_semaphore.Release();

			return result.Entity;
		}
	}
}
