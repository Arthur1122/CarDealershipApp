using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Domain
{
   public class Client
   {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PasportId { get; set; }
        public List<Car> Cars { get; set; }
        public static Client CreateClient(string name, string pasport)
        {
            return new Client
            {
                Name = name,
                PasportId = pasport,
                Cars = new List<Car>(),
            };
        }

        public Client()
        {
            
        }
    }
}
