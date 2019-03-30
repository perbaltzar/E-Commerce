using System;

namespace ECommerce.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }

        internal CartItem ToList()
        {
            throw new NotImplementedException();
        }
    }
}