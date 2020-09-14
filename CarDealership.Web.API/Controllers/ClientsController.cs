using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealership.Web.API.ViewModels;
using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarDealership.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            this._clientRepository = clientRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClientViewModel>> GetClients()
        {
            var clients = _clientRepository.ClientsList();
            List<ClientViewModel> viewClients = new List<ClientViewModel>();
            List<CarViewModel> viewCars = null;

            if (clients.Count > 0)
            {
                foreach (var client in clients)
                {
                    if (client.Cars.Count > 0)
                    {
                        viewCars = new List<CarViewModel>();
                        foreach (var car in client.Cars)
                        {
                            viewCars.Add(new CarViewModel
                            {
                                Number = car.Number,
                                Price = car.Price,
                                IsSold = car.IsSold
                            });
                        }
                    }
                    viewClients.Add(new ClientViewModel
                    {
                        Name = client.Name,
                        PasportId = client.PasportId,
                        Cars = viewCars
                    });
                }
            }
            return Ok(viewClients);
        }

        [HttpGet("{passportId}")]
        public ActionResult<ClientViewModel> GetClient(string passportId)
        {
            var client = _clientRepository.FindClient(passportId);
            ClientViewModel viewClient = null;
            List<CarViewModel> viewCars = null;

            if (client is null)
            {
                return StatusCode(404, "There is not such client");
            }
            
            if(client.Cars.Count > 0)
            {
                viewCars = new List<CarViewModel>();
                foreach (var car in client.Cars)
                {
                    viewCars.Add(new CarViewModel
                    {
                        Number = car.Number,
                        Price = car.Price,
                        IsSold = car.IsSold
                    });
                }
            }

            viewClient = new ClientViewModel
            {
                Name = client.Name,
                PasportId = client.PasportId,
                Cars = viewCars,
            };
            return Ok(viewClient);
        }
    }
}
