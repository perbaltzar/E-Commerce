using System;
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
    }
}
