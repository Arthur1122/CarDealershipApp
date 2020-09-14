using CarDealership.Web.API.ViewModels;
using CarDealershipApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealership.Web.API.ExtensionMethods
{
    public static class CarViewModelExtensionsS
    {
        public static CarViewModel CreateCarModel(this Car carModel, Car car)
        {
            return new CarViewModel
            {
                Number = car.Number,
                IsSold = car.IsSold,
                Price = car.Price,
                ClientName = car.Client != null ? car.Client.Name : null,
                ClientPassportId = car.Client != null ? car.Client.PasportId : null
            };
        }

    }
}
