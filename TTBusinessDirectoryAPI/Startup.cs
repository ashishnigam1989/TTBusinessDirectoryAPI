using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTBusinessDirectoryAPI.Extensions;
using ApplicationService;
using DatabaseService.DbEntities;
using ApplicationService.IServices;
using ApplicationService.Services;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TTBusinessDirectoryAPI
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
           
            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());
            });
            //
             

            //Swagger
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Implement Swagger UI",
                    Description = "A simple example to Implement Swagger UI",
                });
            });

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Dependencies Mapping Start
            services.AddScoped<BusinessDirectoryDBContext>();
            services.AddScoped<IAccount, ApplicationService.Services.Account>();
            services.AddScoped<IMaster, ApplicationService.Services.Master>();
            services.AddScoped<ICompanies, ApplicationService.Services.Companies>();
            services.AddScoped<IOffers, ApplicationService.Services.Offers>();
            services.AddScoped<ICategories, ApplicationService.Services.Categories>();

            //Dependencies Mapping End
            services.AddDbContext<BusinessDirectoryDBContext>(ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAllHeaders");
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            // global error handler
            app.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}
