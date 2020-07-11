using CarDealershipApp.Interface;
using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Commands.Client
{
    public abstract class ClientCommand : Command
    {
        protected IClientRepository _clientRepository;
        public ClientCommand(IClientRepository clientRepo)
        {
            _clientRepository = clientRepo;
        }
    }
}
