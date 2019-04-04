using System;
using System.Transactions;
using ECommerce.Models;
using ECommerce.Repositories;
using ECommerce.Services;
using NUnit.Framework;

namespace ECommerce.IntegrationTests.Services
{
    public class CartServiceTests
    {
        private CartService cartService;

        [SetUp]
        public void SetUp()
        {
            this.cartService = new CartService(new CartRepository("Server=localhost;port=8889;Database=ecommerce;Uid=root;Pwd=root;"),
                                                new CartItemRepository("Server=localhost;port=8889;Database=ecommerce;Uid=root;Pwd=root;"));
        }

        [Test]
        public void Get_ReturnsResultsFromDatabase()
        {
            // Arrange
            const int ExpectedProductId = 6;
            const int ExpectedQuantity = 1; 

            var cartItem = new CartItem
            {
                ProductId = ExpectedProductId,
                Quantity = ExpectedQuantity
            };
            Cart ExpectedCart;
            Cart results;
            // Act

            using (new TransactionScope())
            {
                ExpectedCart = cartService.Create(cartItem);
                results = this.cartService.Get(ExpectedCart.Id);
            }

            // Assert
            Assert.That(results.Id, Is.EqualTo(ExpectedCart.Id));
        }
    }
}
