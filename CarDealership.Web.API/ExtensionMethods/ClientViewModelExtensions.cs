using CarDealership.Web.API.ViewModels;
using CarDealershipApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealership.Web.API.ExtensionMethods
{
    public static class ClientViewModelExtensions
    {
        public static ClientViewModel CreateClientModel(this Client clientModel, Client client)
        {
            return new ClientViewModel
            {
                Name = client.Name,
                PasportId = client.PasportId,
                Cars = GetCars(client)
            };
            
        }

        private static List<CarViewModel> GetCars(Client client)
        {
            if (client.Cars.Count > 0)
            {
                List<CarViewModel> viewCars = new List<CarViewModel>();
                foreach (var car in client.Cars)
                {
                    viewCars.Add(car.CreateCarModel(car));
                }
                return viewCars;
            }
            return null;
        }

    }

    
}
