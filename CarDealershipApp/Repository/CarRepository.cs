using CarDealershipApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealershipApp.Repository
{
    public class CarRepository
    {
        private LinkedList<Car> _cars;
        
        public CarRepository()
        {
            _cars = new LinkedList<Car>();
        }

        public int Count()
        {
            return _cars.Where(c=>c.IsSold == false).ToList().Count;
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
            _cars.AddLast(car);
            return true;
        }

        public bool Sell(string number)
        {
            if(this.List().Where(c=>c.Number == number && c.IsSold == false).Any())
            {
                return true;
            }
            return false;
        }
    }
}
