using CarDealershipApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Interface
{
    public interface ICarRepository
    {
        int Count();
        LinkedList<Car> List();
        bool Add(Car car);
        Car FindCar(string number);
        void SellCar(Car car, Client client);
    }
}
