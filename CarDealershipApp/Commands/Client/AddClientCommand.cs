using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Commands.Client
{
    public class AddClientCommand : ClientCommand
    {
        public AddClientCommand(IClientRepository clientRepository) : base(clientRepository) {}
        public override string CommandText()
        {
            return "add client";
        }

        public override CommandResult Execute()
        {
            Console.WriteLine("Plesae enter client full name");
            string clientName = Console.ReadLine();
            Console.WriteLine("Please enter client pasport id");
            string pasportId = Console.ReadLine();
            Domain.Client client = new Domain.Client(clientName, pasportId);
            bool success = _clientRepository.AddClient(client);
            string message = "Client added successfuly";
            if (!success)
            {
                message = $"Client with this info {clientName} {pasportId} already exists";
            }

            return new CommandResult(success, message);
        }
    }
}
