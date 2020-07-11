using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using CarDealershipApp.Domain;
using CarDealershipApp.Interface;

namespace CarDealershipApp.Commands.Car
{
    public class AddCarCommand : CarCommand
    {
        public AddCarCommand(ICarRepository carRepository) : base(carRepository) { }

        public override string CommandText()
        {
            return "add car";
        }

        public override CommandResult Execute()
        {
            Console.WriteLine("Car number: ");
            string number = Console.ReadLine();
            Console.WriteLine("Car price: ");
            string str = Console.ReadLine();
            decimal price = Decimal.Parse(str);
            Domain.Car car = new Domain.Car(number,price);
            bool success = _carRepository.Add(car);
            string message = "Car added successfully";
            if (!success)
            {
                message = $"Car with number {number} already exists";
            }
            return new CommandResult(success, message);
        }
    }
}
