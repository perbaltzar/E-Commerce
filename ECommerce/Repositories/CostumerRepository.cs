using System;
using Dapper;
using ECommerce.Models;
using MySql.Data.MySqlClient;

namespace ECommerce.Repositories
{
    public class CostumerRepository
    {
        private readonly string connectionString;
        public CostumerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Customer Get (int id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                var customer = connection.QuerySingleOrDefault<Customer>("SELECT * FROM Customers WHERE id = @id", new { id });

                return customer;
            }
        }
    }
}
