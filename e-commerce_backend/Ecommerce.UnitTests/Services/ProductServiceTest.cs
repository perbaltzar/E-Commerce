using System;
using System.Transactions;
using ECommerce.Models;
using ECommerce.Repositories;
using ECommerce.Services;
using NUnit.Framework;

namespace Ecommerce.UnitTests.Services
{
    public class ProductServiceTest
    {
        private ProductService productService;

        [SetUp]
        public void SetUp()
        {
            this.productService = new ProductService(new ProductRepository("Server=localhost;port=8889;Database=ecommerce;Uid=root;Pwd=root;"));
        }

        [Test]
        public void Get_ReturnsResultFromRepository()
        {
            // Arrange

            // Act
            var results = productService.Get();
            // Assert
            Assert.That(results, Is.Not.Null);

        }

        [Test]
        public void Get_GivenValidId_ReturnsIt()
        {
            // Arrange
            const int ExpectedId = 6;

            // Act
            Product results;
            using (new TransactionScope())
            {
                results = productService.Get(ExpectedId);
            }


            // Assert
            Assert.That(results.Id, Is.EqualTo(ExpectedId));

        }
    }
}
