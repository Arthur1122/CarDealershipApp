using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealershipApp.Repository
{
    public class ContractRepository : IContractRepository
    {
        private long _currentContractId = 0;
        private List<Contract> _contracts;

        public ContractRepository()
        {
            _contracts = new List<Contract>();
        }
        public List<Contract> Contracts()
        {
            return _contracts;
        }
        public int Count()
        {
            return _contracts.Count;
        }
        public Contract FindContract(long contractId)
        {
           return Contracts().SingleOrDefault(c => c.Id == contractId);
        }

        public void AddContract(Contract contract)
        {
            contract.Id = ++_currentContractId;
            _contracts.Add(contract); 
        }


    }
}
