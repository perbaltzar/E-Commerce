using System;
using Dapper;
using ECommerce.Models;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Collections.Generic;

namespace ECommerce.Repositories
{
    public class CartItemRepository
    {
        private readonly string connectionString;
        public CartItemRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<CartItem> Get(int id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.Query<CartItem>("SELECT * FROM CartItems WHERE CartId = @CartId", new { id }).ToList();
            }
        }

        public List<Product> GetItemsAsProducts (int id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.Query<Product>("SELECT * FROM CartItems c INNER JOIN Products p ON c.ProductId = p.Id WHERE c.CartId = @id", new { id }).ToList();
           }
        }

        public CartItem Get(CartItem cartItem)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<CartItem>("SELECT * FROM CartItems WHERE CartId = @CartId AND ProductId = @ProductId", cartItem);
            }
        }

        public void Remove(CartItem cartItem)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute("DELETE FROM CartItems WHERE CartId = @CartId AND ProductId = @ProductId", cartItem);
            }
        }
        // Update an item in Cart
        public void UpdateQuantity(CartItem cartItem)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute(
                "UPDATE CartItems SET Quantity = @quantity WHERE ProductId = @productId AND CartId = @cartId",
                cartItem);
            }
        }
    }
}
