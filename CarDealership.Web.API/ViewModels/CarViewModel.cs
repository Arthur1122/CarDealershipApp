using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealership.Web.API.ViewModels
{
    public class CarViewModel
    {
        public string Number { get; set; }
        public bool IsSold { get; set; }
        public decimal Price { get; set; }
        public string ClientName { get; set; }
        public string  ClientPassportId { get; set; }
    }
}
