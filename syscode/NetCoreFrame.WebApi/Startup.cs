using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using NetCoreFrame.WebApi.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebApi
{
    public class Startup
    {

        private readonly string CorsAny = "_corsAny";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<Water_GasService, Water_GasService>();
            services.AddScoped<Water_QualityService, Water_QualityService>();
            services.AddScoped<Frame_UserService, Frame_UserService>();
            #region cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsAny,
                    policy =>
                    {
                        policy.WithOrigins("*")
                        .SetIsOriginAllowed(origin => true)
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS");
                    });
            });
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "水质监测", Version = "v1" });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "水质监测"));
            }

            app.UseRouting();

            #region UseCors
            app.UseCors(CorsAny);
            #endregion
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                //如果设置根目录为swagger,将此值置空
                options.RoutePrefix = string.Empty;
            });
            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
