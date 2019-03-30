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
        public OrderItemRepository orderItemRepository;

        public OrderService(OrderRepository orderRepository, CartRepository cartRepository, 
                            CostumerRepository costumerRepository, OrderItemRepository orderItemRepository)
        {
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
            this.costumerRepository = costumerRepository;
            this.orderItemRepository = orderItemRepository;
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

            order.Customer = costumerRepository.Get(order.CustomerId);
           //order.OrderItems = 
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
            var orderId = orderRepository.Create(customer);

            // Creating OrderItems
            foreach(Product product in cart.Products){
                orderItemRepository.Add(product, orderId);
            }

            // Updating price of Order
            orderRepository.SetPrice(cart, orderId);

            // Getting Order
            var order = orderRepository.Get(orderId);

            // Removing Cart
            cartRepository.Remove(cart.Id);

            // Populating Order with Items and Customer
            order.OrderItems = orderItemRepository.Get(orderId);
            order.Customer = costumerRepository.Get(order.CustomerId);


            return order;
        }

    }
}
