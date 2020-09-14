using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealership.Web.API.ExtensionMethods;
using CarDealership.Web.API.ViewModels;
using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarDealership.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractRepository _contractRepository;
        private readonly ICarRepository _carRepository;
        private readonly IClientRepository _clientRepository;

        public ContractsController(IContractRepository contractRepository, ICarRepository carRepository, IClientRepository clientRepository)
        {
            _contractRepository = contractRepository;
            _carRepository = carRepository;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContractViewModel>> GetContracts()
        {
            var contracts = _contractRepository.Contracts();
            List<ContractViewModel> viewContracts = null;
            if (contracts.Count() > 0)
            {
                viewContracts = new List<ContractViewModel>();
                foreach (var contract in contracts)
                {
                    viewContracts.Add(contract.CreateContractModel(contract));
                }
            }
            return Ok(viewContracts);
        }

        [HttpGet("{contractId}")]
        public ActionResult<ContractViewModel> GetContract(long contractId)
        {
            var contract = _contractRepository.FindContract(contractId);
            if (contract is null)
            {
                return StatusCode(404, "There is not such contract");
            }
            return Ok(contract.CreateContractModel(contract));
        }

        [HttpPost("SellCar")]
        public IActionResult SellCar([FromBody] ContractViewModel contract)
        {
            var car = _carRepository.FindCar(contract.CarNumber);
            if (car is null)
            {
                return StatusCode(404, "The car was not found");
            }
           
            var client = _clientRepository.FindClient(contract.ClientPassportId);
            if (client is null)
            {
                client = new Client
                {
                    Name = contract.ClientName,
                    PasportId = contract.ClientPassportId
                };
                _clientRepository.AddClient(client);
            }
            try
            {
                _carRepository.SellCar(car, new Client
                {
                    Name = contract.ClientName,
                    PasportId = contract.ClientPassportId
                });
                _contractRepository.AddContract(new Contract
                { 
                  Car = car,
                  Client = client,
                  Type = contract.Type,
                  TotalCost = contract.TotalCost,
                  FirstPayment = contract.FirstPayment,
                  Months = contract.Months,
                  MonthsPayment = contract.MonthsPayment,
                  CarId = car.Id,
                  ClientId = client.Id

                });
                return Ok("Car suucessfully sold");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "There is something wrong in the system. Please try again later :(");
            }
        }

        
    }
}
