using ApplicationService.IServices;
using ApplicationService.Services;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using CommonService.Helpers;
using DatabaseService.DbEntities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTBusinessAdminPanel.Extensions;
using TTBusinessDirectoryAPI.Extensions;

namespace TTBusinessAdminPanel
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
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddMvc();
            services.AddControllersWithViews();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Dependencies Mapping Start
            services.AddScoped<BusinessDirectoryDBContext>();
            services.AddScoped<IAccount, Account>();
            services.AddScoped<IMaster, Master>();
            services.AddScoped<ILocation, Location>();
            services.AddScoped<ICompanies, ApplicationService.Services.Companies>();
            services.AddScoped<IOffers, ApplicationService.Services.Offers>();
            services.AddDbContext<BusinessDirectoryDBContext>(ServiceLifetime.Scoped);
            //Dependencies Mapping End

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            ExtensionHelper.Help(accessor);
            AccessAppSettings.Initialize(Configuration);
            app.UseStaticFiles();
            var logger = LogManager.GetLogger("Global");
            app.ConfigureExceptionHandler(logger);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseNotyf();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
