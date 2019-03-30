using System;
using ECommerce.Models;
using ECommerce.Repositories;
using ECommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private string connectionString;
        private CartService cartService;

        public CartController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
            this.cartService = new CartService(new CartRepository(this.connectionString), new CartItemRepository(this.connectionString));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cart), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Cart), StatusCodes.Status404NotFound)]
        public IActionResult Get (int id)
        {
            var cart = cartService.Get(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody]CartItem cartItem)
        {

            if (cartItem.CartId < 1)
            {
                var cart = cartService.Create(cartItem);
                return Ok(cart);
            }
            // Default value zero if no cartId excist in LocalStorage
            else
            {
                var cart = cartService.Add(cartItem);
                return Ok(cart);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Remove([FromBody]CartItem cartItem)
        {
            cartService.Remove(cartItem);
            return Ok();
        }

        //ADD an update
        public IActionResult Update(CartItem cartItem)
        {
            cartService.Update(cartItem);
            return Ok();
        }
    }
}
