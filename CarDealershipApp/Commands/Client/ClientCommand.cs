using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Commands.Client
{
    public abstract class ClientCommand : Command
    {
        protected ClientRepository _clientRepository;
        public ClientCommand(ClientRepository clientRepo)
        {
            _clientRepository = clientRepo;
        }
    }
}
