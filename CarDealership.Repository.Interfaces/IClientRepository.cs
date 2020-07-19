using CarDealershipApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Interface
{
    public interface IClientRepository
    {
        List<Client> ClientsList();
        Client FindClient(string passportId);
        int Count();
        bool AddClient(Client client);

    }
}
