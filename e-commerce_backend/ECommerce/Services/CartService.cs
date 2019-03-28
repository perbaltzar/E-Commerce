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


        public Cart Create(CartItem cartItem)
        {
            // Creating a cart
            var cartId = cartRepository.Create();
            // Adding the item to the cart
            cartItem.CartId = cartId;
            cartRepository.Add(cartItem);

            // Selecting cart and items in it. 
            var cart = cartRepository.Get(cartId);

            return cart;
        }

        public Cart Add(CartItem cartItem)
        {
            cartRepository.Add(cartItem);
            var cart = cartRepository.Get(cartItem.CartId);
            return cart;
        }

        public void Remove(int productId, int cartId)
        {
            cartRepository.Remove(productId, cartId);
        }

        public void Update(int productId, int cartId, int quantity)
        {
            cartRepository.UpdateQuantity(productId, cartId, quantity);
        }
    }
}
