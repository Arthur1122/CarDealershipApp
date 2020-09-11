using CarDealership.Common;
using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealership.Repository.EF
{
    public class EfClientRepository : IClientRepository
    {
        private List<Client> _clients;
        private int _count = 0;
        CarDealershipContext _dbContext;
        public EfClientRepository(CarDealershipContext dbContext)
        {
            _dbContext = dbContext;
            _clients = new List<Client>();
        }

        public bool AddClient(Client client)
        {
            Client foundClient = FindClient(client.PasportId);
            if (foundClient is null)
            {
                ++_count;
                _dbContext.Clients.Add(client);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Client> ClientsList()
        {
            _clients = _dbContext.Clients
                             .Include(c => c.Cars)
                             .ToList();
            return _clients;
        }

        public int Count()
        {
            if (_count == 0 && _clients.Count() > 1)
            {
                _count = _clients.Count();
            }
            return _count;
        }

        public Client FindClient(string passportId)
        {
            return _dbContext.Clients.Where(c => c.PasportId == passportId)
                                              .Include(x => x.Cars)
                                              .FirstOrDefault();
        }
    }
}
