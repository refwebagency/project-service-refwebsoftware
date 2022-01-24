using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using project_service_refwebsoftware.Data;
using project_service_refwebsoftware.Controllers;
using project_service_refwebsoftware.AsyncDataServices;
using project_service_refwebsoftware.EventProcessing;

namespace project_service_refwebsoftware
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
            // Instanciation des bases de donnée local ( phase de test )
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("project"));
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("client"));
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("projecttype"));

            services.AddScoped<IProjectRepo, ProjectRepo>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpClient<ProjectController>();
            services.AddHostedService<MessageBusSuscriber>();
            services.AddTransient<IEventProcessor, EventProcessor>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "project_service_refwebsoftware", Version = "v1" });
            });


            Console.WriteLine("URL todo service: " + Configuration["TodoService"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectService v1"));
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
