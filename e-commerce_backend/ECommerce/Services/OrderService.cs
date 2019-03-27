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
            var order = orderRepository.Get(id);
            order.Cart = cartRepository.Get(order.CartId);
            order.Customer = costumerRepository.Get(order.CustomerId);
            return order;

        }
        public Order Create (Cart cart, Customer customer)
        {
            // Getting the new Id for order and creating customer.
            var orderId =  orderRepository.Create(cart, customer);
            var order = orderRepository.Get(orderId);
            order.Cart = cartRepository.Get(order.CartId);
            order.Customer = costumerRepository.Get(order.CustomerId);
            return order;
        }

    }
}
