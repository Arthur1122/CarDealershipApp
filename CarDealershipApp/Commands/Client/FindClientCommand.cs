using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealershipApp.Commands.Client
{
    public class FindClientCommand : ClientCommand
    {
        public FindClientCommand(IClientRepository clientRepository):base(clientRepository){ }
        public override string CommandText()
        {
            return "find client";
        }

        public override CommandResult Execute()
        {
            Console.WriteLine("Please enter client pasport id");
            string pasportId = Console.ReadLine();
            Domain.Client client = _clientRepository.FindClient(pasportId);
            bool success = true;
            string message = "";

            if (client == null)
            {
                message = $"Client with this info {pasportId} not found";
                success = false;
            }
            message = $"Here are client's info \nName: {client.Name}  PasportId: {client.PasportId}";
            foreach (var car in client.Cars)
            {
                message += $"\nNumber of car: {car.Number} Price: {car.Price}";
            }


            return new CommandResult(success, message);
        }
    }
}
