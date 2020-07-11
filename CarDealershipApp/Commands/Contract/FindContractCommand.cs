using CarDealershipApp.Interface;
using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Commands.Contract
{
    public class FindContractCommand : ContractCommand
    {
        public FindContractCommand(IContractRepository contractRepository):base(contractRepository) { }
        
        public override string CommandText()
        {
            return "find contract";
        }

        public override CommandResult Execute()
        {
            Console.WriteLine("Please enter contract id");
            long contractId = long.Parse(Console.ReadLine());
            Domain.Contract contract = _contractRepository.FindContract(contractId);
            bool success = true;
            string message = "";

            if (contract == null)
            {
                message = $"Contract with this id: {contractId} was not found";
                success = false;
            }
            message = $"Here are contract's info with id: {contract.Id}\nclient name: {contract.Client.Name}\nPasportId: {contract.Client.PasportId}\nCar number: {contract.Car.Number}\nprice: {contract.Car.Price}";
            return new CommandResult(success, message);
        }
    }
}
