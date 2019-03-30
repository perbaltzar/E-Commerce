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
                cart.Products = connection.Query<Product>("SELECT * FROM CartItems c INNER JOIN Products p ON c.ProductId = p.Id WHERE c.CartId = @id", new { id }).ToList();
                return cart;
            }
        }

        // Creating a new cart if not excisting
        public int Create()
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                var cartId = connection.QuerySingle<int>(@"INSERT INTO Carts (Ordered) VALUES (false);
                                                            SELECT LAST_INSERT_ID()");
                return cartId;
            }
        }

        // Adding item to Cart
        public void Add (CartItem cartItem)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {   
                connection.Execute(
                "INSERT INTO CartItems (CartId, ProductId, Quantity) VALUES (@cartId, @productId, @quantity)",
                cartItem);
            }
        }

        // Removing a Cart and its Items
        public void Remove (int id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute(
                    @"DELETE FROM Carts WHERE Id = @Id;
                    DELETE FROM CartItems WHERE CartId = @Id", 
                    new { id });

            }
        }



        // Update an item in Cart
        public void UpdateOrderStatus(int cartId)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute(
                "UPDATE Carts SET Ordered = 1 WHERE Id = @cartId",
                new { cartId });
            }
        }
    }
}
