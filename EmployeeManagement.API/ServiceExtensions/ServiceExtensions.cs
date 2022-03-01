using EmployeeManagement.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.BLL.Core.IConfiguration;
using WebAPI.BLL.Data;

namespace EmployeeManagement.API.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlServerConnection(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<WebApiContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        public static void ConfigureRepositoryPattern(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureJWTSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSetting = configuration.GetSection("JWTSettings");

            services.Configure<JwtSetting>(jwtSetting);
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;


            }).AddEntityFrameworkStores<WebApiContext>();
        }
    }
}
