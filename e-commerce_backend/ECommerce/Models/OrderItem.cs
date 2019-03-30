using System;
namespace ECommerce.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CartId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public OrderItem()
        {
        }
    }
}
