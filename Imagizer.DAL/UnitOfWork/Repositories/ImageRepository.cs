using Imagizer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Imagizer.DAL.UnitOfWork.Repositories
{
	class ImageRepository : EfRepository<Image>, IImageRepository
	{
		public ImageRepository(DbSet<Image> dbSet, Func<Task> saveChangesDelegate, SemaphoreSlim semaphore) : base(dbSet, saveChangesDelegate, semaphore)
		{
		}
	}
}
