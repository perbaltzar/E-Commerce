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
            var cart = cartRepository.Get(id);
            cart.Products = cartItemRepository.GetItemsAsProducts(id);
            return cart;
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
            cart.Products = cartItemRepository.GetItemsAsProducts(cartId);

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
            cart.Products = cartItemRepository.GetItemsAsProducts(cart.Id);

            return cart;

        }

        public bool RemoveItem(CartItem cartItem)
        {
            if (cartItem.Id < 0 || cartItem.CartId < 0)
            {
                return false;
            }
            cartItemRepository.Remove(cartItem);
            return true;
        }

        public void Update(CartItem cartItem)
        {

            cartItemRepository.UpdateQuantity(cartItem);
        }
    }
}
