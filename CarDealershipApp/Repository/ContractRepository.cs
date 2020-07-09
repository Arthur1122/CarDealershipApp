using CarDealershipApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealershipApp.Repository
{
    public class ContractRepository
    {
        private long _currentContractId = 0;
        private List<Contract> _contracts;

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
            if(this.Contracts().Where(c=>c.ContractId == contractId).Any())
            {
                return this.Contracts().Where(c => c.ContractId == contractId).First();
            }
            return null;
        }

        public void AddContract(Contract contract)
        {
            if (_contracts == null)
                _contracts = new List<Contract>();

            contract.ContractId = ++_currentContractId;
            _contracts.Add(contract); 
        }


    }
}
