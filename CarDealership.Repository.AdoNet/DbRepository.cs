using CarDealership.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Text;

namespace CarDealershipApp.DbRepository
{
    public abstract class DbRepository
    {
        private SqlOptions _sqlOptions;
        public DbRepository(SqlOptions sqlOptions)
        {
            _sqlOptions = sqlOptions;
        }

        protected SqlConnection GetConnection() 
        {
            SqlConnection connection = new SqlConnection(_sqlOptions.ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
