using CarDealership.Common;
using CarDealership.Repository.EF;
using CarDealershipApp.DbRepository;
using CarDealershipApp.Interface;
using CarDealershipApp.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace CarDealershipApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            
            var appSection = configuration.GetSection(nameof(AppOptions));
            var sqlSection = configuration.GetSection(nameof(SqlOptions));
            var str_appMode = appSection["AppMode"];
            AppMode appMode = Enum.Parse<AppMode>(str_appMode);

            ServiceCollection services = new ServiceCollection();

            switch (appMode)
            {
                case AppMode.InMemory:
                    services.AddSingleton<ICarRepository, CarRepository>();
                    services.AddSingleton<IClientRepository, ClientRepository>();
                    services.AddSingleton<IContractRepository, ContractRepository>();
                    break;
                case AppMode.AdoNet:
                    services.AddSingleton (new SqlOptions { ConnectionString = sqlSection["ConnectionStringAdoNet"]});
                    services.AddSingleton<ICarRepository, DbCarRepository>();
                    services.AddSingleton<IClientRepository, DbClientRepository>();
                    services.AddSingleton<IContractRepository, DbContractRepository>();
                    break;
                case AppMode.EfCore:
                    services.AddDbContext<CarDealershipContext>(c => c.UseSqlServer(sqlSection["ConnectionStringEfCore"]));
                    services.AddSingleton<ICarRepository, EfCarRepository>();
                    services.AddSingleton<IClientRepository, EfClientRepository>();
                    services.AddSingleton<IContractRepository, EfContractRepository>();
                    break;
                default:
                    break;
            }
            services.AddSingleton<General>();

            var serviceProvider = services.BuildServiceProvider();
            
            General general = serviceProvider.GetRequiredService<General>();
            general.Start();
        }
    }
}
