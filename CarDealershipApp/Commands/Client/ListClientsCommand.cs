using CarDealershipApp.Domain;
using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CarDealershipApp.Commands.Client
{
    public class ListClientsCommand : ClientCommand
    {
        public ListClientsCommand(ClientRepository clientRepository): base(clientRepository){}
        public override string CommandText()
        {
            return "clients list";
        }

        public override CommandResult Execute()
        {
            Console.WriteLine("______________________________");
            foreach (Domain.Client client in _clientRepository.ClientsList())
            {
                if(client.Cars == null || client.Cars.Count < 1)
                    Console.WriteLine($"Name: {client.Name}  PasportId: {client.PasportId}");
                else
                {
                    string cars = "";
                    foreach (var item in client.Cars)
                    {
                        cars += $" {item.Number},";
                    }
                    Console.WriteLine($"Name: {client.Name}  PasportId: {client.PasportId} Numbers of cars: {cars}");
                }
                    

                Console.WriteLine("______________________________");
            }
            return new CommandResult(true, $"Listed {_clientRepository.Count()} clients");
        }
    }
}
