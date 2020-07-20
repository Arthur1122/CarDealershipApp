using CarDealership.Common;
using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace CarDealershipApp.DbRepository
{
    public class DbCarRepository : DbRepository,ICarRepository
    {
        private LinkedList<Car> _cars;
        private int _count = 0;

        public DbCarRepository(SqlOptions sqlOptions):base(sqlOptions)
        {
            _cars = new LinkedList<Car>();
        }

        public bool Add(Car car)
        {
            Car foundCar = FindCar(car.Number);
            if (foundCar == null)
            {
                ++_count;
                using (var connection = GetConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand(
                        "INSERT INTO Cars (Number,IsSold,Price,ClientId) " +
                        $"VALUES('{car.Number}', {car.IsSold.GetHashCode()} ,{car.Price},null)", connection);
                    sqlCommand.ExecuteNonQuery();
                }
                return true;
            }
            return false;
        }

        public int Count()
        {
            if(_count == 0 && _cars.Where(c => c.IsSold == false).Count() > 1)
            {
                _count = _cars.Where(c => c.IsSold == false).Count();
            }
            return _count;
        }

        public Car FindCar(string number)
        {
            Car car = null;
            using (var connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * "+
                    "FROM Cars LEFT JOIN " +
                    "Clients ON Cars.ClientId = Clients.ClientId "+
                    $"WHERE Number='{number}'", connection);
                SqlDataReader sdr = sqlCommand.ExecuteReader();
                if (sdr.Read())
                {
                    car = new Car { Id = (int)sdr["CarId"], Number = (string)sdr["Number"], IsSold = (bool)sdr["IsSold"], Price = (decimal)sdr["Price"]};
                    car.ClientId = Convert.IsDBNull(sdr["ClientId"]) ? null : (int?)sdr["ClientId"];
                    if(car.ClientId != null)
                    {
                        car.Client = new Client { Id = (int)sdr["ClientId"], Name = (string)sdr["Name"], PasportId = (string)sdr["PasportId"] };
                        car.Client.Cars.Add(car);
                    }
                       
                    return car;
                }
                else
                    return car;

            }
        }

        public LinkedList<Car> List()
        {
            using (var connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * FROM Cars", connection);
                SqlDataReader sdr = sqlCommand.ExecuteReader();
                while (sdr.Read())
                {
                    _cars.AddLast(new Car
                    {
                        Id = (int)sdr["CarId"],
                        Number = (string)sdr["Number"],
                        IsSold = (bool)sdr["IsSold"],
                        Price = (decimal)sdr["Price"],
                        ClientId = Convert.IsDBNull(sdr["ClientId"]) ? 0 : (int)sdr["ClientId"]
                    }) ;
                }
                return _cars;

            }
        }
        // anel verjum
        public void SellCar(Car car, Client client)
        {
            --_count;
            car.IsSold = true;
            client.Cars.Add(car);

            using (var connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    "UPDATE Cars "+
                    $"SET IsSold = {car.IsSold.GetHashCode()}, ClientId = {client.Id} " +
                    $"WHERE Cars.CarId = {car.Id}", connection);
                sqlCommand.ExecuteNonQuery();
            }
        }
    
    }
}
