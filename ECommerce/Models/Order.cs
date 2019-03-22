using System;
using ECommerce.Models;

namespace ECommerce
{
    public class Order
    {

        public int CustomerId { get; set; }
        public int TotalPrice { get; set; }
        public int Id { get; set; }
        public Cart Cart { get; set; }
        public Customer Customer { get; set; }

        public Order()
        {
        }
    }
}
