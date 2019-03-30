using System;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services
{
    public class CartService
    {
        private CartRepository cartRepository { get; set; }
        private CartItemRepository cartItemRepository;

        public CartService(CartRepository cartRepository, CartItemRepository cartItemRepository)
        {
            this.cartRepository = cartRepository;
            this.cartItemRepository = cartItemRepository;
        }

        public Cart Get (int id)
        {
            if (id < 0)
            {
                return null;
            }
            return cartRepository.Get(id);
        }


        public Cart Create(CartItem cartItem)
        {
            // CartItem conditions
            if (cartItem.ProductId < 0 || cartItem.Quantity < 1)
            {
                return null;
            }

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
            // CartItem conditions
            if (cartItem.ProductId < 0 || cartItem.Quantity < 1 || cartItem.CartId < 0)
            {
                return null;
            }

            // Check if item exists in database
            var oldCartItem = this.cartItemRepository.Get(cartItem);
            if (oldCartItem != null)
            {
                cartItem.Quantity += oldCartItem.Quantity;
                this.cartItemRepository.Remove(oldCartItem);
            }

            // Adding Item to cartItem
            cartRepository.Add(cartItem);

            // Adding Item to Cart
            var cart = cartRepository.Get(cartItem.CartId);
            return cart;
        }

        public bool Remove(CartItem cartItem)
        {
            if (cartItem.Id < 0 || cartItem.CartId < 0)
            {
                return false;
            }
            cartRepository.Remove(cartItem);
            return true;
        }

        public void Update(CartItem cartItem)
        {

            cartRepository.UpdateQuantity(cartItem);
        }
    }
}
