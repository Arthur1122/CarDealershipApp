using CarDealership.Common;
using CarDealershipApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealership.Repository.EF
{
    public class CarDealershipContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        public CarDealershipContext(DbContextOptions<CarDealershipContext> dbContextOptions) : base (dbContextOptions)
        {

        }
    }
}
