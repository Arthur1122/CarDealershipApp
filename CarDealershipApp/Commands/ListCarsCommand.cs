﻿using CarDealershipApp.Domain;
using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealershipApp.Commands
{
    public class ListCarsCommand : CarCommand
    {
        public ListCarsCommand(CarRepository carRepository) : base(carRepository) { }

        public override string CommandText()
        {
            return "list cars";
        }

        public override CommandResult Execute()
        {
            Console.WriteLine("______________________________");
            foreach (Car car in _carRepository.List().Where(c=>c.IsSold == false))
            {
                Console.WriteLine(car.Number);
                Console.WriteLine("______________________________");
            }
            return new CommandResult(true, $"Listed {_carRepository.Count()} cars");
        }
    }
}
