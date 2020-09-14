using CarDealershipApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealership.Web.API.ViewModels
{
    public class ContractViewModel
    {
        public long ContractId { get; set; }
        public decimal FirstPayment { get; set; }
        public short? Months { get; set; }
        public decimal? MonthsPayment { get; set; }
        public ContractType Type { get; set; }
        public decimal TotalCost { get; set; }
        public string CarNumber { get; set; }
        public string ClientName { get; set; }
        public string ClientPassportId { get; set; }
    }
}
