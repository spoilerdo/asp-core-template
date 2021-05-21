using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Template_Service.Config.Contexts;
using Template_Service.Config.Models;
using Template_Service.Persistence.Repositories;
using Template_Service.Services;

namespace Template_Service {
    public class Startup {
        public IConfiguration Configuration { get; }
        private readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";

        public Startup(IConfiguration configuration) {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            var settings = new Settings();
            Configuration.Bind(nameof(Settings), settings);

            services.AddGrpc();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IMongoTemplateRepository, MongoTemplateRepository>();
        }

        //Can be overridden in a derived class and also only be called from within this class or any derived classess
        protected virtual void ConfigureMongo(IServiceCollection services, Settings settings) {
            services.Configure<MongoSettings>(options => {
                options.ConnectionString = settings.Database.ConnectionString;
                options.Database = settings.Database.Name;
            });
        }

        protected virtual void ConfigureSql(IServiceCollection services, Settings settings) {
            services.AddDbContext<MySqlDbContext>(options => {
                var connectionString = Configuration.GetConnectionString("AnomalyDbConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), builder => {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            });
        }

        protected virtual void ConfigureCors(IServiceCollection services) {
            services.AddCors(options => {
                options.AddPolicy(AllowSpecificOrigins,
                builder => {
                    builder.AllowAnyOrigin()
                        .WithMethods("POST", "OPTIONS")
                        .AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(AllowSpecificOrigins);
            app.UseGrpcWeb();
            app.UseEndpoints(endpoints => {
                endpoints.MapGrpcService<TemplateService>().EnableGrpcWeb();

                endpoints.MapGet("/", async context => {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
