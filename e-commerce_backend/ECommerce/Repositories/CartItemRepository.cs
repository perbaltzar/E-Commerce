using System;
using Dapper;
using ECommerce.Models;
using MySql.Data.MySqlClient;

namespace ECommerce.Repositories
{
    public class CartItemRepository
    {
        private readonly string connectionString;
        public CartItemRepository(string connectionString)
        {
            this.connectionString = connectionString;
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
    }
}
