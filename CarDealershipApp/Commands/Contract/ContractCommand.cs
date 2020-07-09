using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Commands.Contract
{
    public abstract class ContractCommand : Command
    {
        protected ContractRepository _contractRepository;
        public ContractCommand(ContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }
    }
}
