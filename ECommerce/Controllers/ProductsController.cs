using System;
using System.Collections.Generic;
using ECommerce.Models;
using ECommerce.Repositories;
using ECommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly string connectionString;
        private readonly ProductsService productsService;

        public ProductsController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
            this.productsService = new ProductsService(new ProductsRepository(connectionString));
        }




        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var products = this.productsService.Get();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var product = this.productsService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody]Product product)
        {
            var postIsSuccessful = this.productsService.Add(product);
            if (postIsSuccessful)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deleteIsSuccessful = this.productsService.Delete(id);
           if (deleteIsSuccessful)
            {
                return Ok();
            }
            return BadRequest();
        }
    }

}
