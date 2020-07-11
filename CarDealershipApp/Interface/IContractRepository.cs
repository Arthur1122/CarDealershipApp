using CarDealershipApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Interface
{
    public interface IContractRepository
    {
        List<Contract> Contracts();
        int Count();
        Contract FindContract(long contractId);
        void AddContract(Contract contract);
    }
}
