using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Imagizer.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Imagizer", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Imagizer v1"));
			}

			app.UseRouting();

			app.UseMiddleware(typeof(ApiKeyMiddleware));
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		public void ConfigureContainer(IUnityContainer container)
		{
			BLL.Facade.SetupDependencies(container);

			container.RegisterInstance(SetupMapper());
		}

		private IMapper SetupMapper()
		{
			MapperConfiguration config = new MapperConfiguration(cfg => RegisterMappings(cfg));

			config.AssertConfigurationIsValid();

			return new Mapper(config);
		}

		private void RegisterMappings(IMapperConfigurationExpression mapperConfiguration)
		{
			mapperConfiguration.EnableEnumMappingValidation();

			BLL.Facade.RegisterMappings(mapperConfiguration);

			mapperConfiguration.CreateMap<BLL.Models.Images.Image, Models.API.V1.Images.Image>();
			mapperConfiguration.CreateMap<BLL.Models.Images.DeleteErrorCode, Models.API.V1.Images.DeleteErrorCode>();
			mapperConfiguration.CreateMap<Models.API.V1.Images.AddImageData, BLL.Models.Images.AddImageData>();
		}
	}
}
