using System;
using ECommerce.Models;

namespace ECommerce
{
    public class Order
    {
        public Cart Cart { get; set; }
        public int CustomerId { get; set; }
        public int TotalPrice { get; set; }
        public int Id { get; set; }

        public Order()
        {
        }
    }
}
