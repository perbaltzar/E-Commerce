using System;
namespace ECommerce
{
    public class Order
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public int TotalPrice { get; set; }
        public int Id { get; set; }

        public Order()
        {
        }
    }
}
