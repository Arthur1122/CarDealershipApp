using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipApp.Domain
{
    public class Contract
    {
        public long Id { get; set; }
        public decimal FirstPayment { get; set; }
        public short? Months { get; set; }
        public decimal? MonthsPayment { get; set; }
        public ContractType Type { get; set; }
        public decimal TotalCost { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public static Contract CreateContract(Car car,Client client)
        {
            return new Contract
            {
                Car = car,
                Client = client,
            };
        }
        public Contract()
        {

        }
    }

    public enum ContractType
    {
        Debit,
        Credit
    }
}
