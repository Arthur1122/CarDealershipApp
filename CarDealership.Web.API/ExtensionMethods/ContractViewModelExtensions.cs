using CarDealership.Web.API.ViewModels;
using CarDealershipApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealership.Web.API.ExtensionMethods
{
    public static class ContractViewModelExtensions
    {
        public static ContractViewModel CreateContractModel(this Contract contractModel, Contract contract)
        {
            return new ContractViewModel
            {
                ContractId = contract.Id,
                CarNumber = contract.Car.Number,
                TotalCost = contract.Car.Price,
                Months = contract.Months,
                MonthsPayment = contract.MonthsPayment,
                Type = contract.Type,
                FirstPayment = contract.FirstPayment,
                ClientName = contract.Client.Name,
                ClientPassportId = contract.Client.PasportId
            };
        }
    }
}
