using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Domain
{
    public class Car
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public bool IsSold { get; set; }
        public Client Client { get; set; }
        public int? ClientId { get; set; }
        public decimal Price { get; set; }
        public static Car CreateCar(string number, decimal price)
        {
            return new Car
            {
                Number = number,
                Price = price,
            };
        }
        public Car()
        {

        }
    }
}
