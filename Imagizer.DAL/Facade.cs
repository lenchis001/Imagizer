using Imagizer.DAL.UnitOfWork.Repositories;
using Unity;

namespace Imagizer.DAL
{
	public static class Facade
	{
		public static void SetupDependencies(IUnityContainer container)
		{
			container.RegisterSingleton<IImageRepository, ImageRepository>();
		}
	}
}
