using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Domain
{
    public class Contract
    {
        public long ContractId { get; set; }
        public decimal FirstPayment { get; set; }
        public short? Months { get; set; }
        public decimal? MonthsPayment { get; set; }
        public ContractType Type { get; set; }
        public decimal TotalCost { get; set; }
        public Car Car { get; set; }
        public Client Client { get; set; }

        public Contract(Car car,Client client)
        {
            Car = car;
            Client = client;
        }
    }

    public enum ContractType
    {
        Debit = 0,
        Credit
    }
}
