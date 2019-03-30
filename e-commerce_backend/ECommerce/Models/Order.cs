using System;
using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce
{
    public class Order
    {

        public int CustomerId { get; set; }
        public int Price { get; set; }
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Cart Cart { get; set; }
        public Customer Customer { get; set; }

        public Order()
        {
        }
    }
}