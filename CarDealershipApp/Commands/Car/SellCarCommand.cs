using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace CarDealershipApp.Commands.Car
{
    public class SellCarCommand : CarCommand
    {
        private IClientRepository _clientRepository;
        private IContractRepository _contractRepository;
        public SellCarCommand(ICarRepository carRepository,IClientRepository clientRepository,IContractRepository contractRepository) : base(carRepository) 
        {
            _clientRepository = clientRepository;
            _contractRepository = contractRepository;
        }

        public override string CommandText()
        {
            return "sell car";
        }

        public override CommandResult Execute()
        {
            Console.WriteLine("Please enter number of selling car");
            string number = Console.ReadLine();
            Console.WriteLine("Please enter name of cleint");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter pasportID of cleint");
            string pasportId = Console.ReadLine();
            bool isSell = _carRepository.Sell(number);
            string message = "";
            bool isKeptContract = true;
            if (!isSell)
            {
                message = $"There is not such car with number of {number}";
            }
            else
            {
                Domain.Car car = _carRepository.FindCar(number);
                Domain.Client client = _clientRepository.FindClient(pasportId);

                if (client == null)
                {
                    client = new Domain.Client(name, pasportId);
                    _clientRepository.AddClient(client);
                    car.Client = client;
                }
                else
                    car.Client = client;

                _carRepository.SellCar(car,client);
               
                _clientRepository.AddClient(client);

                isKeptContract  = KeepContract(car,client);
                message = $"The number of {number} car was sold succesfully";
            }
            if(!isKeptContract)
            {
                isSell = false;
                message = "There are something wrong plesae try again";
            }

            return new CommandResult(isSell, message); 

        }

        private bool KeepContract(Domain.Car car, Domain.Client client)
        {
            Domain.Contract contract = new Domain.Contract(car, client);
            Console.WriteLine("Please enter the contract type is Debit or Credit ?");
            string type = Console.ReadLine();
            try
            {
                if (type.ToLower() == ContractType.Debit.ToString().ToLower())
                {
                    contract.TotalCost = car.Price;
                    contract.Type = ContractType.Debit;
                    contract.FirstPayment = contract.TotalCost;   
                }
                else
                {
                    Console.WriteLine("How much months will contract keep?");
                    short months = short.Parse(Console.ReadLine());
                    contract.Months = months;
                    contract.FirstPayment = car.Price / contract.Months.Value;
                    contract.Type = ContractType.Credit;
                    contract.MonthsPayment = contract.FirstPayment;
                    contract.TotalCost = car.Price;
                }
                _contractRepository.AddContract(contract);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
            
        }
    }
}
