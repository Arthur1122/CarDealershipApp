using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Domain
{
   public class Client
   {
        public string Name { get; set; }
        public string PasportId { get; set; }
        public List<Car> Cars { get; set; }
        public Client(string name, string pasport)
        {
            Name = name;
            PasportId = pasport;
        }
        public Client()
        {

        }
    }
}
