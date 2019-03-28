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

        // Removing an item from Cart
        public void Remove (int productId, int cartId)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute(
                "DELETE FROM CartItems WHERE ProductId = @productId AND CartId = @cartId", 
                new { productId, cartId });
            }
        }

        // Update an item in Cart
        public void UpdateQuantity (int productId, int cartId, int quantity)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute(
                "UPDATE CartItems SET Quantity = @quantity WHERE ProductId = @productId AND CartId = @cartId", 
                new { quantity, productId, cartId });
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

        public void UpdatePrice (Cart cart)
        {
            var price = cart.Products.Sum(item => item.Price * item.Quantity);
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute(
                "UPDATE Carts SET Price = @price WHERE Id = @Id",
                new { price, cart.Id });
            }
        }
    }
}
