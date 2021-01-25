using Imagizer.DAL.UnitOfWork.Repositories;
using MyStudio.Server.DAL.UnitOfWork;
using Unity;

namespace Imagizer.DAL
{
	public static class Facade
	{
		public static void SetupDependencies(IUnityContainer container)
		{
			container.RegisterSingleton<IUnitOfWork, EfUnitOfWork>();

			container.RegisterInstance<IUserRepository>(container.Resolve<IUnitOfWork>().UserRepository);
			container.RegisterInstance<IImageRepository>(container.Resolve<IUnitOfWork>().ImageRepository);
		}
	}
}
