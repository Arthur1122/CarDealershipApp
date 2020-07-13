using CarDealershipApp.Domain;
using CarDealershipApp.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CarDealershipApp.DbRepository
{
    public class DbCarRepository : DbRepository,ICarRepository
    {
        private LinkedList<Car> _cars;
        private int _count = 0;
        private int _carId = 0;

        public DbCarRepository(SqlOptions sqlOptions):base(sqlOptions)
        {
            _cars = new LinkedList<Car>();
        }

        public bool Add(Car car)
        {
            Car foundCar = FindCar(car.Number);
            if (foundCar == null)
            {
                car.Id = ++_carId;
                ++_count;
                using (var connection = GetConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand(
                        "INSERT INTO Cars (Number,IsSold,ClientId,Price)" +
                        $"VALUES('{car.Number}',{car.IsSold.GetHashCode()},{car.ClientId},{car.Price})", connection);
                    sqlCommand.ExecuteNonQuery();
                }
                return true;
            }
            return false;
        }

        public int Count()
        {
            return _cars.Where(c=>c.IsSold == false).Count();
        }

        public Car FindCar(string number)
        {
            using (var connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    "SELECT *"+
                    "FROM Cars" +
                    $" WHERE Number='{number}'", connection);
                SqlDataReader sdr = sqlCommand.ExecuteReader();
                if (sdr.Read())
                {
                    return new Car { Id = (int)sdr["Id"], Number = (string)sdr["Number"], IsSold = (bool)sdr["IsSold"], Price = (decimal)sdr["Price"], ClientId = (int)sdr["ClientId"] };
                }
                else
                    return null;

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
                    _cars.AddLast(new Car { Id = (int)sdr["Id"], Number = (string)sdr["Number"], IsSold = (bool)sdr["IsSold"], Price = (decimal)sdr["Price"], ClientId = (int)sdr["ClientId"] });
                }
                return _cars;

            }
        }

        public void SellCar(Car car, Client client)
        {
            --_count;
            car.IsSold = true;
            client.Cars.Add(car);

            using (var connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(
                    "UPDATE Cars"+
                    $"SET IsSold = {car.IsSold.GetHashCode()}, ClientId = {client.Id}", connection);
            }
        }
    
    }
}
