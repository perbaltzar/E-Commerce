using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using ECommerce.Models;
using MySql.Data.MySqlClient;

namespace ECommerce.Repositories
{
    public class CartRepository
    {
        private readonly string connectionString;

        // Constructor
        public CartRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Get Cart and CartItems
        public Cart Get (int id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                var cart = connection.QuerySingleOrDefault<Cart>("SELECT * FROM carts WHERE id = @id", new { id });
                cart.CartItems = connection.Query<CartItem>("SELECT * FROM CartItems WHERE CartId = @id", new { id }).ToList();
                return cart;
            }
        }

        // Creating a new cart if not excisting
        public int Create(int productId)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.Execute("INSERT INTO Carts OUTPUT Inserted.ID DEFAULT VALUES ");
            }
        }

        // Adding item to Cart
        public void Add (int productId, int cartId)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                var item = connection.Execute("INSERT INTO CartItems (CartId, ProductId) VALUES (@cartId, @productId)", new { productId, cartId });
            }
        }

        // Removing an item from Cart
        public void Remove (int id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute("DELETE FROM CartItems WHERE id = @id", new { id });
            }
        }


    }
}
