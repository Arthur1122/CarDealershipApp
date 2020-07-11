using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Domain
{
   public class Client
   {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasportId { get; set; }
        public List<Car> Cars { get; set; }
        public Client(string name, string pasport) : this()
        {
            Name = name;
            PasportId = pasport;
        }
        public Client()
        {
            Cars = new List<Car>();
        }
    }
}
