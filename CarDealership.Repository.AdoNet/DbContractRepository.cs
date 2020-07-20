using CarDealership.Common;
using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CarDealershipApp.DbRepository
{
    public class DbContractRepository : DbRepository, IContractRepository
    {
        private List<Contract> _contracts;
        private int _count = 0;
        public DbContractRepository(SqlOptions sqlOptions) : base(sqlOptions)
        {
            _contracts = new List<Contract>();
        }

        public void AddContract(Contract contract)
        {
            string months = "";
            string monthsPayment = "";
            ++_count;
            if (contract.Months == null)
                months = "null";
            else
                months = contract.Months.ToString();

            if (contract.MonthsPayment == null)
                monthsPayment = "null";
            else
                monthsPayment = contract.MonthsPayment.ToString();

            using (var connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    "INSERT INTO Contracts (FirstPayment,Months,MonthsPayment,TotalCost,Type,CarId,ClientId) " +
                    $"VALUES({contract.FirstPayment},{months},{monthsPayment},{contract.TotalCost},'{contract.Type}',{contract.CarId},{contract.ClientId})", connection);
                sqlCommand.ExecuteNonQuery();
            }
              
        }

        public List<Contract> Contracts()
        {
            Contract contract = null;
            using (var connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * " +
                    "FROM Contracts LEFT JOIN Clients " +
                    "ON Contracts.ClientId = Clients.ClientId " +
                    "LEFT JOIN Cars " +
                    "ON Contracts.CarId = Cars.CarId ", connection);
                SqlDataReader sdr = sqlCommand.ExecuteReader();
                while (sdr.Read())
                {
                    if(contract == null || contract.Id != (long)sdr["ContractId"])
                    {
                        contract = new Contract
                        {
                            Id = (long)sdr["ContractId"],
                            FirstPayment = (decimal)sdr["FirstPayment"],
                            Months = Convert.IsDBNull(sdr["Months"]) ? null : (short?)sdr["Months"],
                            MonthsPayment = Convert.IsDBNull(sdr["MonthsPayment"]) ? null : (decimal?)sdr["MonthsPayment"],
                            TotalCost = (decimal)sdr["TotalCost"],
                            CarId = (int)sdr["CarId"],
                            ClientId = (int)sdr["ClientId"]
                        };
                        _contracts.Add(contract);
                    }
                   
                    if ((string)sdr["Type"] == ContractType.Credit.ToString())
                        contract.Type = ContractType.Debit;
                    else
                        contract.Type = ContractType.Credit;
                    contract.Car = new Car { Id = (int)sdr["CarId"], Number = (string)sdr["Number"], IsSold = (bool)sdr["IsSold"], Price = (decimal)sdr["Price"], ClientId = (int)sdr["ClientId"] };
                    contract.Client = new Client { Id = (int)sdr["ClientId"], Name = (string)sdr["Name"], PasportId = (string)sdr["PasportId"] };

                    contract.Car.Client = contract.Client;
                    contract.Client.Cars.Add(contract.Car);

                }
                return _contracts;
            }
        }

        public int Count()
        {
            if(_count == 0 && _contracts.Count > 0)
            {
                _count = _contracts.Count;
            }
           return _count;
        }

        public Contract FindContract(long contractId)
        {
            Contract contract = null;
            using (var connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * " +
                    "FROM Contracts LEFT JOIN Clients " +
                    "ON Contracts.ClientId = Clients.ClientId " +
                    "LEFT JOIN Cars " +
                    "ON Contracts.CarId = Cars.CarId " +
                    $"WHERE Contracts.ContractId ='{contractId}'", connection);
                SqlDataReader sdr = sqlCommand.ExecuteReader();
                if (sdr.Read())
                {
                    contract = new Contract { 
                        Id = (long)sdr["ContractId"],
                        FirstPayment = (decimal)sdr["FirstPayment"],
                        Months = Convert.IsDBNull(sdr["Months"])? null : (short?)sdr["Months"],
                        MonthsPayment = Convert.IsDBNull(sdr["MonthsPayment"]) ? null : (decimal?)sdr["MonthsPayment"],
                        TotalCost = (decimal)sdr["TotalCost"],
                        CarId = (int)sdr["CarId"],
                        ClientId = (int)sdr["ClientId"]
                    };
                    if ((string)sdr["Type"] == ContractType.Credit.ToString())
                        contract.Type = ContractType.Debit;
                    else
                        contract.Type = ContractType.Credit;
                    contract.Car = new Car { Id = (int)sdr["CarId"], Number = (string)sdr["Number"], IsSold = (bool)sdr["IsSold"], Price = (decimal)sdr["Price"], ClientId = (int)sdr["ClientId"] };
                    contract.Client = new Client { Id = (int)sdr["ClientId"], Name = (string)sdr["Name"], PasportId = (string)sdr["PasportId"] };

                    contract.Car.Client = contract.Client;
                    contract.Client.Cars.Add(contract.Car);
                }
                return contract;
            }
        }
    }
}
