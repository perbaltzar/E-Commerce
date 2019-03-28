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

        public int Create(Customer customer)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute("INSERT INTO Customers (Name, Adress, ZipCode, City, Country) VALUES (@Name, @Adress, @ZipCode, @City, @Country)", customer);
                return connection.QuerySingleOrDefault<int>("SELECT Id FROM Customers ORDER BY Id DESC LIMIT 1");
            }
        }
    }
}
