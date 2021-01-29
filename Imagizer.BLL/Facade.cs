using AutoMapper;
using Imagizer.BLL.Models.Images;
using Imagizer.BLL.Services;
using Unity;

namespace Imagizer.BLL
{
	public static class Facade
	{
		public static void SetupDependencies(IUnityContainer container)
		{
			DAL.Facade.SetupDependencies(container);

			container.RegisterSingleton<IImageService, ImageService>();
			container.RegisterSingleton<IFileMetadataService, FileMetadataService>();
		}

		public static void RegisterMappings(IMapperConfigurationExpression cfg)
		{
			cfg.CreateMap<DAL.Models.Image, Image>();
			cfg.CreateMap<DAL.Models.DeleteErrorCode, DeleteErrorCode>();
			cfg.CreateMap<AddImageData, DAL.Models.AddImageData>();
		}
	}
}
