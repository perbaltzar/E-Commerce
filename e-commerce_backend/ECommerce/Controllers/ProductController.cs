using System;
using System.Collections.Generic;
using ECommerce.Models;
using ECommerce.Repositories;
using ECommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Web.Http.Cors;

namespace ECommerce.Controllers
{
    //[EnableCors(origins: "http://localhost:3000/", headers: "Content-Type: application/json", methods: "*")]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly string connectionString;
        private readonly ProductService productService;

        public ProductController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
            this.productService = new ProductService(new ProductRepository(connectionString));
        }




        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var products = this.productService.Get();
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
            var product = this.productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody]Product product)
        {
            var postIsSuccessful = this.productService.Add(product);
            if (postIsSuccessful == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deleteIsSuccessful = this.productService.Delete(id);
           if (deleteIsSuccessful)
            {
                return Ok();
            }
            return BadRequest();
        }
    }

}
