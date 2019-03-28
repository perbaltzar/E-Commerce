using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using ECommerce.Models;
using MySql.Data.MySqlClient;

namespace ECommerce.Repositories
{
    public class ProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public List<Product> Get()
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.Query<Product>("SELECT * FROM products").ToList();

            }
        }

        public Product Get(int id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.QuerySingleOrDefault<Product>("SELECT * FROM products WHERE id = @id", new { id });

            }
        }

        public int Add(Product product)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                return connection.Execute(@"INSERT INTO Products (name, description, price, typeid, balance, imageurl) 
                                            VALUES (@name, @description, @price, @typeid, @balance, @imageurl);
                                            SELECT LAST_INSERT_ID()", product);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Execute("DELETE FROM products WHERE id = @id", new { id });
            }
        }
    }
}
