using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CarDealershipApp.DbRepository
{
    public class DbClientRepository : DbRepository,IClientRepository
    {
        private List<Client> _clients;
        private int _count = 0;
        public DbClientRepository(SqlOptions sqlOptions) : base (sqlOptions)
        {
            _clients = new List<Client>();
        }

        public bool AddClient(Client client)
        {
            Client  foundClient = FindClient(client.PasportId);
            if (foundClient == null)
            {
                ++_count;
                using (var connection = GetConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand(
                        "INSERT INTO Clients (Name,PasportId)" +
                        $"VALUES('{client.Name}','{client.PasportId}')",connection);
                    sqlCommand.ExecuteNonQuery();
                }
                return true;
            }
            return false;
        }

        public List<Client> ClientsList()
        {
            Client client = null;
            Car car = new Car();
            using (var connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * " +
                    "FROM Clients " +
                    "LEFT JOIN Cars " +
                    "ON Clients.ClientId = Cars.ClientId ", connection);
                SqlDataReader sdr = sqlCommand.ExecuteReader();
                while (sdr.Read())
                {
                    if(client == null || client.Id != (int)sdr["ClientId"])
                    {
                        client = new Client { Id = (int)sdr["CLientId"], Name = (string)sdr["Name"], PasportId = (string)sdr["PasportId"] };
                        _clients.Add(client);
                    }
                    car.Id = Convert.IsDBNull(sdr["CarId"]) ? 0 : (int)sdr["CarId"];
                    if(car.Id > 0)
                    {
                        client.Cars.Add(new Car { Id = (int)sdr["CarId"], Number = (string)sdr["Number"], IsSold = (bool)sdr["IsSold"], Price = (decimal)sdr["Price"], ClientId = (int)sdr["ClientId"], Client = client });
                    }
                    
                }
            }
            return _clients;
        }

        public int Count()
        {
            if(_count ==  0 && _clients.Count() > 1)
            {
                _count = _clients.Count();
            }
            return _count;
        }

        public Client FindClient(string passportId)
        {
            Client client = null;
            Car car = new Car();
            using (var connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * "+
                    "FROM Clients "+
                    "LEFT JOIN Cars "+
                    "ON Clients.ClientId = Cars.ClientId "+
                    $"WHERE Clients.PasportId ='{passportId}'", connection);
                SqlDataReader sdr = sqlCommand.ExecuteReader();
                if(sdr.Read())
                {
                    client = new Client { Id = (int)sdr["ClientId"], Name = (string)sdr["Name"], PasportId = (string)sdr["PasportId"] };
                    car.Id = Convert.IsDBNull(sdr["CarId"]) ? 0 : (int)sdr["CarId"];
                    if(car.Id > 0)
                    {
                        client.Cars.Add(new Car { Id = (int)sdr["CarId"], Number = (string)sdr["Number"], IsSold = (bool)sdr["IsSold"], Price = (decimal)sdr["Price"], ClientId = (int)sdr["ClientId"], Client = client });
                        while (sdr.Read())
                        {
                            client.Cars.Add(new Car { Id = (int)sdr["CarId"], Number = (string)sdr["Number"], IsSold = (bool)sdr["IsSold"], Price = (decimal)sdr["Price"], ClientId = (int)sdr["ClientId"], Client = client });
                        }
                    }
                    
                }
                
                return client;
            }
        }
    }
}
