using System;
using System.Collections.Generic;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services
{
    public class OrderService
    {
        public OrderRepository orderRepository { get; set; }
        public CartRepository cartRepository;
        public CostumerRepository costumerRepository;

        public OrderService(OrderRepository orderRepository, CartRepository cartRepository, CostumerRepository costumerRepository)
        {
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
            this.costumerRepository = costumerRepository;
        }

        public List<Order> Get ()
        {
            return orderRepository.Get();

        }
        public Order Get (int id)
        {
            // Conditions
            if (id < 0)
            {
                return null;
            }
            // Getting Order
            var order = orderRepository.Get(id);

            // Populating Order with Cart and Customer
            order.Cart = cartRepository.Get(order.CartId);
            order.Customer = costumerRepository.Get(order.CustomerId);

            return order;

        }
        public Order Create (Cart cart, Customer customer)
        {
            // Cart Conditions
            if (cart.Id < 0 || cart.Ordered)
            {
                return null;
            }

            // Customer Conditions
            if (string.IsNullOrEmpty(customer.Name) || string.IsNullOrEmpty(customer.Adress) ||
                string.IsNullOrEmpty(customer.City) || string.IsNullOrEmpty(customer.Country) ||
                string.IsNullOrEmpty(customer.ZipCode))
            {
                return null;
            }

            // Creating Customer
            customer.Id = costumerRepository.Create(customer);

            //Creating Order
            var orderId = orderRepository.Create(cart, customer);

            // Getting Order
            var order = orderRepository.Get(orderId);

            // Updating order Status
            cartRepository.UpdateOrderStatus(order.CartId);



            // Updating price of cart
            this.cartRepository.UpdatePrice(cart);


            // Populating Order with Cart and Customer
            order.Cart = cartRepository.Get(order.CartId);
            order.Customer = costumerRepository.Get(order.CustomerId);


            return order;
        }

    }
}
