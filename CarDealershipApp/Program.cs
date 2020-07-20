using CarDealership.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDealershipApp
{
    public class Program
    {
        static void Main(string[] args)
        {

            General general = new General(new  SqlOptions { ConnectionString = "data source=(localdb)\\MSSQLLocalDB; database=CarDealership; integrated security=SSPI" }, new AppOptions { });
            general.Start();
        }
    }
}
