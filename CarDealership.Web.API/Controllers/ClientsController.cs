using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealership.Web.API.ExtensionMethods;
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
            List<ClientViewModel> viewClients = null;
            if (clients.Count > 0)
            {
                viewClients = new List<ClientViewModel>();
                foreach (var client in clients)
                {
                    viewClients.Add(client.CreateClientModel(client));
                }
            }
            return Ok(viewClients);
        }

        [HttpGet("{passportId}")]
        public ActionResult<ClientViewModel> GetClient(string passportId)
        {
            var client = _clientRepository.FindClient(passportId);

            if (client is null)
            {
                return StatusCode(404, "There is not such client");
            }
            var viewClient = client.CreateClientModel(client);
            return Ok(viewClient);
        }
    }
}
