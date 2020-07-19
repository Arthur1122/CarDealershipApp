using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CarDealershipApp.Repository
{
    public class ClientRepository : IClientRepository
    {
        private List<Client> _clients;
        private int _clientId = 0;
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
            return ClientsList().SingleOrDefault(c => c.PasportId == passportId);
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
            client.Id = ++_clientId;
            _clients.Add(client);
            return true;
        }
    }
}
