using CarDealershipApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CarDealershipApp.Repository
{
    public class ClientRepository
    {
        private List<Client> _clients;

        public ClientRepository()
        {
            _clients = new List<Client>();
        }

        public List<Client> ClientsList()
        {
            return _clients;
        }

        public Client FindClient(string passportId)
        {
            if (this.ClientsList().Where(c => c.PasportId == passportId).Any())
            {
                return this.ClientsList().Where(c => c.PasportId == passportId).FirstOrDefault();
            }
            return null;
        }

        public int Count()
        {
            return _clients.Count;
        }

        public bool AddClient(Client client)
        {
            if (_clients.Where(c => c.PasportId == client.PasportId).Any())
            {
                return false;
            }
            _clients.Add(client);
            return true;
        }

        public void AddCar(string pasportId,Car car)
        {
            Client client = this.FindClient(pasportId);
            if (client.Cars == null)
                client.Cars = new List<Car>();

            client.Cars.Add(car);
        }


    }
}
