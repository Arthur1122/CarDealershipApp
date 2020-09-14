using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealership.Common;
using CarDealership.Repository.EF;
using CarDealershipApp.DbRepository;
using CarDealershipApp.Interface;
using CarDealershipApp.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace CarDealership.Web.API
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
            var appSection = Configuration.GetSection(nameof(AppOptions));
            var sqlSection = Configuration.GetSection(nameof(SqlOptions));
            var str_appMode = appSection["AppMode"];
            AppMode appMode = Enum.Parse<AppMode>(str_appMode);
            switch (appMode)
            {
                case AppMode.InMemory:
                    services.AddSingleton<ICarRepository, CarRepository>();
                    services.AddSingleton<IClientRepository, ClientRepository>();
                    services.AddSingleton<IContractRepository, ContractRepository>();
                    break;
                case AppMode.AdoNet:
                    services.AddSingleton(new SqlOptions { ConnectionString = sqlSection["ConnectionStringAdoNet"] });
                    services.AddSingleton<ICarRepository, DbCarRepository>();
                    services.AddSingleton<IClientRepository, DbClientRepository>();
                    services.AddSingleton<IContractRepository, DbContractRepository>();
                    break;
                case AppMode.EfCore:
                    services.AddDbContext<CarDealershipContext>(c => c.UseSqlServer(sqlSection["ConnectionStringEfCore"]));
                    services.AddTransient<ICarRepository, EfCarRepository>();
                    services.AddTransient<IClientRepository, EfClientRepository>();
                    services.AddTransient<IContractRepository, EfContractRepository>();
                    break;
                default:
                    break;
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarDelearship API", Version = "v1" });
            });

            services.AddControllers();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarDelearship API V1");
            });
        }
    }
}
