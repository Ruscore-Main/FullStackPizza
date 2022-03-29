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
using Newtonsoft.Json.Serialization;
using PizzaProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace PizzaProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static string SqlConnectionIntegratedSecurity
        {
            get
            {
                var sb = new SqlConnectionStringBuilder
                {
                    DataSource = @"(localdb)\MSSQLLocalDB",
                    // Подключение будет с проверкой подлинности пользователя Windows
                    IntegratedSecurity = true,
                    // Название целевой базы данных.
                    InitialCatalog = "PizzaData"
                };
                return sb.ConnectionString;
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Добавление CORS
            services.AddCors(c => {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
                .AllowAnyHeader());
            });


            // устанавливаем контекст данных
            services.AddDbContext<PizzaContext>(options => options.UseSqlServer(SqlConnectionIntegratedSecurity));

            // JSON сериализер
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                   .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()); 

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseDeveloperExceptionPage();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
            });
        }
    }
}
