using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Order> Get()
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.Query<Order>("SELECT * FROM Orders").ToList();
                // Need to get all customers and carts for every order

            }
        }

        public Order Get (int id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<Order>("SELECT * FROM Orders WHERE id = @id", new { id });
                    
            }
        }

        public int Create(Cart cart, Customer customer)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                //Calculate TotalPrice
                var totalPrice = 0;
                connection.Execute("INSERT INTO Customers (Name, Adress, ZipCode, City, Country) VALUES (@Name, @Adress, @ZipCode, @City, @Country)", customer);
                var customerId = connection.QuerySingleOrDefault<int>("SELECT Id FROM Customers ORDER BY Id DESC LIMIT 1");
                var cartId = cart.Id;
                connection.Execute("INSERT INTO Orders (CartId, CustomerId, TotalPrice) VALUES (@cartId, @customerId, @totalPrice)",
                new { cartId, customerId, totalPrice });
                var orderId = connection.QuerySingleOrDefault<int>("SELECT Id FROM Orders ORDER BY Id DESC LIMIT 1");
                return orderId;
            }
        }
    }
}
