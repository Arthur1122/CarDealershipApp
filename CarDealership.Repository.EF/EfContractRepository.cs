using CarDealership.Common;
using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealership.Repository.EF
{
    public class EfContractRepository : IContractRepository
    {
        private List<Contract> _contracts;
        CarDealershipContext _dbContext;
        public EfContractRepository (CarDealershipContext dbContext)
        {
            _dbContext = dbContext;
            _contracts = new List<Contract>();
        }
        public void AddContract(Contract contract)
        {  
            _dbContext.Contracts.Add(contract);
            _dbContext.SaveChanges();
        }

        public new List<Contract> Contracts()
        {
            _contracts = _dbContext.Contracts.Include(c => c.Car)
                                             .ThenInclude(x => x.Client)
                                             .Include(k => k.Client)
                                             .ThenInclude(j => j.Cars)
                                             .ToList();

            return _contracts;
        }

        public int Count()
        {
            return _contracts.Count;
        }

        public Contract FindContract(long contractId)
        {
            return _dbContext.Contracts.Include(c => c.Car)
                                       .ThenInclude(x => x.Client)
                                       .Include(k => k.Client)
                                       .ThenInclude(j => j.Cars)
                                       .FirstOrDefault();
        }
    }
}
