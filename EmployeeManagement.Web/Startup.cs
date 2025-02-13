using EmployeeManagement.Web.Profiles;
using EmployeeManagement.Web.Repositories.DepartmentRepo.Interfaces;
using EmployeeManagement.Web.Repositories.DepartmentRepo.Services;
using EmployeeManagement.Web.Repositories.EmployeeRepo.Interfaces;
using EmployeeManagement.Web.Repositories.EmployeeRepo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web
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
            services.AddRazorPages();
            
            services.AddServerSideBlazor();
            
            services.AddAutoMapper(typeof(EmployeeProfile));

            services.AddHttpClient<IDepartmentRepositoryService, DepartmentRepositoryService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44377/");
            });
            services.AddHttpClient<IEmployeeRepositoryService, EmployeeRepositoryService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44377/");
            });
        }


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

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
