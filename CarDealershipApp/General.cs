using CarDealership.Common;
using CarDealershipApp.Commands;
using CarDealershipApp.Commands.Car;
using CarDealershipApp.Commands.Client;
using CarDealershipApp.Commands.Contract;
using CarDealershipApp.DbRepository;
using CarDealershipApp.Interface;
using CarDealershipApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp
{
    public class General
    {
        private readonly List<Command> _commands;
        private readonly ICarRepository _carRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IContractRepository _contractRepository;
        public General(SqlOptions sqlOptions, AppOptions appOptions)
        {
            _commands = new List<Command>();
            if (appOptions.Mode == AppMode.AdoNet)
            {
                _carRepository = new DbCarRepository(sqlOptions);
                _clientRepository = new DbClientRepository(sqlOptions);
                _contractRepository = new DbContractRepository(sqlOptions);
            }
            else if(appOptions.Mode == AppMode.InMemory)
            {
                _carRepository = new CarRepository();
                _clientRepository = new ClientRepository();
                _contractRepository = new ContractRepository();
            }
            
            

            // cars
            _commands.Add(new AddCarCommand(_carRepository));
            _commands.Add(new SellCarCommand(_carRepository,_clientRepository,_contractRepository));
            _commands.Add(new ListCarsCommand(_carRepository));
            _commands.Add(new FindCarCommand(_carRepository));
            // clients
            _commands.Add(new AddClientCommand(_clientRepository));
            _commands.Add(new ListClientsCommand(_clientRepository));
            _commands.Add(new FindClientCommand(_clientRepository));
            // Contracts
            _commands.Add(new ListContractCommand(_contractRepository));
            _commands.Add(new FindContractCommand(_contractRepository));

        }

        public void Start()
        {
            Console.WriteLine("Please choose a command: ");
            string commandText = Console.ReadLine();

            while (commandText != "end")
            {
                Command curCommand = null;
                for (int i = 0; i < _commands.Count; ++i)
                {
                    if (_commands[i].CommandText() == commandText)
                    {
                        curCommand = _commands[i];
                        break;
                    }
                }

                if (curCommand == null)
                {
                    ConsoleHelper.WriteLineError("Wrong command, please enter correct command: ");
                }
                else
                {
                    ExecuteCommand(curCommand);
                    Console.WriteLine("Please choose a command: ");
                }

                commandText = Console.ReadLine();
            }
        }

        private void ExecuteCommand(Command command)
        {
            CommandResult commandResult = command.Execute();

            ConsoleColor color = ConsoleColor.Green;
            if (!commandResult.Success)
            {
                color = ConsoleColor.Red;
            }

            ConsoleHelper.WriteLineColored(commandResult.Message, color);
        }
    }
}
