using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Commands.Contract
{
    public class ListContractCommand : ContractCommand
    {
        public ListContractCommand(ContractRepository contractRepository):base(contractRepository)
        {

        }
        public override string CommandText()
        {
            return "list contracts";
        }

        public override CommandResult Execute()
        {
            Console.WriteLine("______________________________");
            foreach (Domain.Contract contract in _contractRepository.Contracts())
            {
                Console.WriteLine($"Contract id: {contract.ContractId}\nclient name: {contract.Client.Name}\nPasportId: {contract.Client.PasportId}\nNumber of car: {contract.Car.Number}\nprice: {contract.Car.Price}");   
                Console.WriteLine("______________________________");
            }
            return new CommandResult(true, $"Listed {_contractRepository.Count()} contracts");
        }
    }
}
