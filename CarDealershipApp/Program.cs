using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDealershipApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            General general = new General(new DbRepository.SqlOptions { ConnectionString = "data source=(localdb)\\MSSQLLocalDB; database=CarDealership; integrated security=SSPI" });
            general.Start();
        }
    }
}
