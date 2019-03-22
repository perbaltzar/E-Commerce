using System;
namespace ECommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public string ImageUrl { get; set; }
        public int Balance { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public Product()
        {

        }
    }
}
