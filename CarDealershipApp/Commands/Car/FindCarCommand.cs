using CarDealershipApp.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Commands.Car
{
    public class FindCarCommand : CarCommand
    {
        public FindCarCommand(ICarRepository carRepository) : base(carRepository) { }
        public override string CommandText()
        {
            return "find car";
        }

        public override CommandResult Execute()
        {
            Console.WriteLine("Please enter car number");
            string number = Console.ReadLine();
            Domain.Car car = _carRepository.FindCar(number);
            bool success = true;
            string message = "";

            if (car == null)
            {
                message = $"Car with this number {number} not found";
                success = false;
            }
            message = $"Here are car's info \nNumber: {car.Number}  Price: {car.Price} IsSold: {car.IsSold}";
            if(car.Client != null)
            {
                message += $"\nClient name: {car.Client.Name} PasportId: {car.Client.PasportId}";
            }
            return new CommandResult(success, message);
        }
    }
}
