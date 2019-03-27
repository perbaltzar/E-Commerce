using System;
namespace ECommerce.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Customer()
        {
        }
    }
}
