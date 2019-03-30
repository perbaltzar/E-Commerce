using System;
using System.Transactions;
using ECommerce.Models;
using ECommerce.Repositories;
using ECommerce.Services;
using NUnit.Framework;

namespace ECommerce.IntegrationTests.Services
{
    public class ProductServiceTests
    {
        private ProductService productService;

        [SetUp]
        public void SetUp()
        {
            this.productService = new ProductService(new ProductRepository("Server=localhost;port=8889;Database=ecommerce;Uid=root;Pwd=root;"));
        }

        [Test]
        public void Get_ReturnsResultsFromDatabase()
        {
            // Act
            var results = this.productService.Get();

            // Assert
            Assert.That(results.Count, Is.AtLeast(11));
            Assert.That(results[0].Id, Is.EqualTo(6));
            Assert.That(results[0].Name, Is.EqualTo("Soda Pop"));
            Assert.That(results[0].Price, Is.EqualTo(1));
        }

        [Test]
        public void Get_GivenId_ReturnsResultsFromDatabase()
        {
            // Arrange
            const string ExpectedName = "Polly";
            const int ExpectedPrice = 3;
            const int ExpectedTypeId = 2;
            const string ExpectedDescription = "Skum med chokladsmak";
            const int Id = 9;


            // Act
            var results = productService.Get(Id);

            // Assert
            Assert.That(results.Name, Is.EqualTo(ExpectedName));
            Assert.That(results.Price, Is.EqualTo(ExpectedPrice));
            Assert.That(results.TypeId, Is.EqualTo(ExpectedTypeId));
            Assert.That(results.Description, Is.EqualTo(ExpectedDescription));
        }

        [Test]
        public void Add_GivenValidProduct_SavesIt()
        {
            // Arrange
            const string ExpectedName = "Vincents Goda";
            const string ExpectedDescription = "Goda godisar";
            const int ExpectedTypeId = 3;
            const int ExpectedPrice = 1;
            const int ExpectedBalance = 1000;
            const string ExpectedImageUrl = "imageUrl";

            var product = new Product
            {
                Name = ExpectedName,
                Description = ExpectedDescription,
                TypeId = ExpectedTypeId,
                Price = ExpectedPrice,
                Balance = ExpectedBalance,
                ImageUrl = ExpectedImageUrl
            };
            Product results;
            int id;

            // Act
            using (new TransactionScope())
            {
                id = productService.Add(product);
                results = productService.Get(id);
            }
        
            // Assert
            //Assert.That(results.Name, Is.EqualTo(ExpectedName));
            //Assert.That(results.Description, Is.EqualTo(ExpectedDescription));
            //Assert.That(results.TypeId, Is.EqualTo(ExpectedTypeId));
            //Assert.That(results.Price, Is.EqualTo(ExpectedPrice));
            //Assert.That(results.Balance, Is.EqualTo(ExpectedBalance));
            //Assert.That(results.ImageUrl, Is.EqualTo(ExpectedImageUrl));




        }

        [Test]
        public void Delete_GivenValidId_DeletesIt()
        {
            // Arrange
            const string ExpectedName = "Vincents Goda";
            const string ExpectedDescription = "Goda godisar";
            const int ExpectedTypeId = 3;
            const int ExpectedPrice = 1;
            const int ExpectedBalance = 1000;
            const string ExpectedImageUrl = "imageUrl";

            var product = new Product
            {
                Name = ExpectedName,
                Description = ExpectedDescription,
                TypeId = ExpectedTypeId,
                Price = ExpectedPrice,
                Balance = ExpectedBalance,
                ImageUrl = ExpectedImageUrl
            };
            int id;
            Product results;

            //Act
            using (new TransactionScope())
            {
                id = productService.Add(product);
                results = productService.Get(id);
            }
          
            // Assert
            Assert.That(results, Is.EqualTo(null));

        }
    }
}