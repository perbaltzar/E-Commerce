using System;
using System.Collections.Generic;
using Dapper;
using ECommerce.Models;
using MySql.Data.MySqlClient;
using System.Linq;

namespace ECommerce.Repositories
{
    public class OrderItemRepository
    {
        private readonly string connectionString;
        public OrderItemRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<OrderItem> Get(int orderId)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.Query<OrderItem>(@"SELECT * FROM OrderItems WHERE OrderId = @orderId", new { orderId }).ToList();
            }
        }

        public void Add (Product product, int orderId)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute(
                    @"INSERT INTO OrderItems (Name, Quantity, OrderId, Price) 
                    VALUES (@Name, @Quantity, @OrderId, @Price)", 
                    new { product.Name, product.Quantity, orderId, product.Price});
            }
        }


    }
}
