using CarDealershipApp.Domain;
using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace CarDealershipApp.Commands
{
    public class SellCarCommand : CarCommand
    {
        private ClientRepository _clientRepository;
        public SellCarCommand(CarRepository carRepository,ClientRepository clientRepository) : base(carRepository) 
        {
            _clientRepository = clientRepository;
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

            if (!isSell)
            {
                message = $"There is not such car with number of {number}";
            }
            else
            {
                Car car = _carRepository.List().Where(c => c.Number == number).FirstOrDefault();
                Client client = _clientRepository.FindClient(pasportId);

                if (client == null)
                {
                    client = new Client(name, pasportId);
                    car.Client = client;
                }
                else
                    car.Client = client;

                car.IsSold = true;
                if (client.Cars == null)
                    client.Cars = new List<Car>();
                client.Cars.Add(car);

                _clientRepository.AddClient(client);

                message = $"The number of {number} car was sold succesfully";
            }
            
            return new CommandResult(isSell, message); 

        }
    }
}
