using CarDealership.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarDealershipApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            AppMode appMode = AppMode.InMemory;
            string connctionString = "";
            var appSection = configuration.GetSection(nameof(AppOptions));
            var sqlSection = configuration.GetSection(nameof(SqlOptions));
            var str_appMode = appSection["AppMode"];
            if(str_appMode == AppMode.AdoNet.ToString())
            {
                connctionString = sqlSection["ConnectionStringAdoNet"];
                appMode = AppMode.AdoNet;
            }

            General general = new General(new  SqlOptions { ConnectionString = connctionString }, new AppOptions { Mode = appMode });
            general.Start();
        }
    }
}
