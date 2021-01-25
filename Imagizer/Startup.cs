using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Imagizer.BLL.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Unity;

namespace Imagizer
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllersWithViews();

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/build";
			});

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.Events.OnRedirectToLogin = context =>
					{
						context.Response.Headers["Location"] = context.RedirectUri;
						context.Response.StatusCode = 401;
						return Task.CompletedTask;
					};
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});
		}

		public void ConfigureContainer(IUnityContainer container)
		{
			BLL.Facade.SetupDependencies(container);

			container.RegisterInstance(SetupMapper(container.Resolve<IHashService>()));
		}

		private IMapper SetupMapper(IHashService hashService)
		{
			MapperConfiguration config = new MapperConfiguration(cfg => RegisterMappings(cfg, hashService));

			config.AssertConfigurationIsValid();

			return new Mapper(config);
		}

		private void RegisterMappings(IMapperConfigurationExpression mapperConfiguration, IHashService hashService)
		{
			mapperConfiguration.EnableEnumMappingValidation();

			BLL.Facade.RegisterMappings(mapperConfiguration);
			mapperConfiguration.CreateMap<BLL.Models.Entities.Images.Image, Models.API.V1.Images.Image>().ReverseMap();

			mapperConfiguration.CreateMap<Models.API.V1.Users.AddUserData, BLL.Models.Entities.Users.AddUserData>()
				.ForMember(dst => dst.PasswordHash, action => action.MapFrom(sourceValue => hashService.MakeHash(sourceValue.Password)));
			mapperConfiguration.CreateMap<BLL.Models.Entities.Users.User, Models.API.V1.Users.User>()
				.ForMember(dst => dst.Password, action => action.Ignore())
				.ReverseMap()
				.ForMember(dst => dst.PasswordHash, action => action.MapFrom(sourceValue => hashService.MakeHash(sourceValue.Password)));
			mapperConfiguration.CreateMap<BLL.Models.DefaultDataFetchResult<BLL.Models.Entities.Users.User>, Models.API.V1.DefaultDataFetchResult<Models.API.V1.Users.User>>();


			mapperConfiguration.CreateMap<BLL.Models.DefaultFetchResult, Models.API.V1.DefaultFetchResult>();
		}
	}
}
