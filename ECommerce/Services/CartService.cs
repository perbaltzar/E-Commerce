using System;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services
{
    public class CartService
    {
        public CartRepository cartRepository { get; set; }

        public CartService(CartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public Cart Get (int id)
        {
            return cartRepository.Get(id);
        }


        public Cart Create(int productId)
        {
            // Creating a cart
            var cartId = cartRepository.Create(productId);
            // Adding the item to the cart
            cartRepository.Add(productId, cartId);
            // Selecting cart and items in it. 
            var cart = cartRepository.Get(cartId);

            return cart;
        }

        public Cart Add(int productId, int cartId)
        {
            cartRepository.Add(productId, cartId);
            var cart = cartRepository.Get(cartId);
            return cart;
        }
    }
}
