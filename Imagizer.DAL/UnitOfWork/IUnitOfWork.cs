using Imagizer.DAL.UnitOfWork.Repositories;

namespace MyStudio.Server.DAL.UnitOfWork
{
	public interface IUnitOfWork
	{
		IUserRepository UserRepository { get; }
		IImageRepository ImageRepository { get; }
	}
}
