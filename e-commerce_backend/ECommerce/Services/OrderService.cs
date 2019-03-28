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
            // Getting Order
            var order = orderRepository.Get(id);

            // Populating Order with Cart and Customer
            order.Cart = cartRepository.Get(order.CartId);
            order.Customer = costumerRepository.Get(order.CustomerId);

            return order;

        }
        public Order Create (Cart cart, Customer customer)
        {
            //Creating Order
            var orderId = orderRepository.Create(cart, customer);

            // Getting Order
            var order = orderRepository.Get(orderId);

            // Updating order Status
            cartRepository.UpdateOrderStatus(order.CartId);

            // Creating Customer
            order.CustomerId = this.costumerRepository.Create(customer);

            // Updating price of cart
            this.cartRepository.UpdatePrice(cart);


            // Populating Order with Cart and Customer
            order.Cart = cartRepository.Get(order.CartId);
            order.Customer = costumerRepository.Get(order.CustomerId);


            return order;
        }

    }
}
