using AutoMapper;
using Imagizer.BLL.Models.Entities.Users;
using Imagizer.BLL.Services;
using Unity;

namespace Imagizer.BLL
{
	public static class Facade
	{
		public static void SetupDependencies(IUnityContainer container)
		{
			DAL.Facade.SetupDependencies(container);

			container.RegisterSingleton<IUserService, UserService>();
			container.RegisterSingleton<IHashService, HashService>();
		}

		public static void RegisterMappings(IMapperConfigurationExpression cfg)
		{
			cfg.CreateMap<DAL.Models.User, User>().ReverseMap();
			cfg.CreateMap<AddUserData, DAL.Models.User>()
				.ForMember(opt => opt.ApiKey, config => config.Ignore())
				.ForMember(opt => opt.Id, config => config.Ignore())
				.ForMember(opt => opt.Images, config => config.Ignore());
		}
	}
}
