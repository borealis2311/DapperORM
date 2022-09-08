using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Context;
using Services.Repository.Customer;
using Services.Repository.FuncInRole;
using Services.Repository.Function;
using Services.Repository.Module;
using Services.Repository.Role;
using Services.Repository.User;
using Services.Repository.UserInRole;

namespace WebAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<DapperContext>();
			services.AddScoped<ICustomerRepository, CustomerRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUirRepository, UirRepository>();
			services.AddScoped<IFirRepository, FirRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();
			services.AddScoped<IModuleRepository, ModuleRepository>();
			services.AddScoped<IFunctionRepository, FunctionRepository>();
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
