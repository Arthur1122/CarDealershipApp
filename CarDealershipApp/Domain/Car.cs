using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Domain
{
    public class Car
    {
        public string Number;
        public bool IsSold { get; set; }
        public Client Client { get; set; }
        public Car(string number)
        {
            Number = number;
        }
    }
}
