using System;
using Dapper;
using ECommerce.Models;
using MySql.Data.MySqlClient;

namespace ECommerce.Repositories
{
    public class OrderRepository
    {
        private readonly string connectionString;

        // Constructor
        public OrderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public Order Create(Cart cart, Customer customer)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute("INSERT INTO Customer (Name, Adress, ZipCode, City, Country) VALUES (@Name, @Adress, @ZipCode, @City, @Country)",  customer );
            }
        }
    }
}
