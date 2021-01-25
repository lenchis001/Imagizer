using Imagizer.DAL.Models;
using Imagizer.DAL.UnitOfWork.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyStudio.Server.DAL.UnitOfWork
{
	public class EfUnitOfWork : DbContext, IUnitOfWork
	{
		private readonly SemaphoreSlim _semaphore;

		public DbSet<User> Users { get; set; }
		public DbSet<Image> Images { get; set; }

		public IUserRepository UserRepository => new UserRepository(Users, SaveChangesAsync, _semaphore);
		public IImageRepository ImageRepository => new ImageRepository(Images, SaveChangesAsync, _semaphore);

		public EfUnitOfWork() : base()
		{
			Database.EnsureCreated();
			_semaphore = new SemaphoreSlim(1);
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var result = await base.SaveChangesAsync(cancellationToken);
			ClearTrackings();

			return result;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasMany(s => s.Images)
				.WithOne(o => o.User);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
#if DEBUG
			optionsBuilder.UseSqlite("Data Source = Imagizer.db");
#else
            optionsBuilder.UseMySql("server=127.0.0.1;database=languagestorm;UserId=leon;Password=grippolek2022``;CharSet=utf8");
#endif
		}

		private void ClearTrackings()
		{
			var entities = ChangeTracker
				.Entries()
				.ToArray();

			foreach (var entity in entities)
			{
				entity.State = EntityState.Detached;
			}
		}

		private Task SaveChangesAsync()
		{
			return SaveChangesAsync(default);
		}
	}
}
