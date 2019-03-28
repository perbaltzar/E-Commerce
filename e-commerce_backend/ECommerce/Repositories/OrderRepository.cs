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
                // Selecting IDs
                var customerId = customer.Id;
                var cartId = cart.Id;

                //Insert into database
                var orderId = connection.Execute(@"INSERT INTO Orders (CartId, CustomerId) VALUES (@cartId, @customerId);
                                                   SELECT LAST_INSERT_ID()", 
                                                   new { cartId, customerId });
                return orderId;
            }
        }
    }
}
