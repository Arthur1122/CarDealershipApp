using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealershipApp.Repository
{
    public class CarRepository : ICarRepository
    {
        private LinkedList<Car> _cars;
        private int _count = 0;
        private int _carId = 0;
        public CarRepository()
        {
            _cars = new LinkedList<Car>();
        }

        public int Count()
        {
            return _count;
        }

        public LinkedList<Car> List()
        {
            return _cars;
        }

        public bool Add(Car car)
        {
            if(_cars.Where(c=>c.Number == car.Number).Any())
            {
                return false;
            }
            car.Id = ++_carId;
            _cars.AddLast(car);
            ++_count;
            return true;
        }
        public Car FindCar(string number)
        {
            return List().SingleOrDefault(c => c.Number == number);
        }
        public bool Sell(string number)
        {
            if(this.List().Where(c=>c.Number == number && c.IsSold == false).Any())
            {
                return true;
            }
            return false;
        }

        public void SellCar(Car car, Client client)
        {
            --_count;
            car.IsSold = true;
            client.Cars.Add(car);
        }
    }
}
