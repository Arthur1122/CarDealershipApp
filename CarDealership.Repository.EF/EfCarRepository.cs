using CarDealership.Common;
using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CarDealership.Repository.EF
{
    public class EfCarRepository : ICarRepository
    {
        
        private LinkedList<Car> _cars;
        private int _count = 0;
        private CarDealershipContext _dbContext;
        public EfCarRepository(CarDealershipContext dbContext)
        {
            _dbContext = dbContext;
            _cars = new LinkedList<Car>();
        }
        public bool Add(Car car)
        {
            Car foundCar = FindCar(car.Number);
            if (foundCar is null)
            {
                ++_count;
                _dbContext.Cars.Add(car);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public int Count()
        {
            if (_count == 0 && _cars.Where(c => c.IsSold == false).Count() > 1)
            {
                _count = _cars.Where(c => c.IsSold == false).Count();
            }
            return _count;
        }

        public Car FindCar(string number)
        {
            return _dbContext.Cars.Where(c => c.Number == number && c.IsSold == false)
                                  .Include(x=>x.Client)
                                  .FirstOrDefault();
        } 

        public LinkedList<Car> List()
        {
            var cars = _dbContext.Cars.
                                  Include(c => c.Client)
                                  .ToList();

            foreach (var car in cars)
            {
                _cars.AddLast(car);
            }

            return _cars;
        }

        public void SellCar(Car car, Client client)
        {
            --_count;
            car.IsSold = true;
            //client.Cars.Add(car);

            _dbContext.Cars.Update(car);
            //_dbContext.Clients.Update(client);
            _dbContext.SaveChanges();
        }
    }
}
