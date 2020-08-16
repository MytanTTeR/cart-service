using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CartService.Domain;
using CartService.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [ApiController]
    [Route("cart-items")]
    public class CartItemController : ControllerBase
    {
        public const string CartIdSessionKey = "CartId";

        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartService cartService, ICartItemService cartItemService)
        {
            _cartService = cartService;
            _cartItemService = cartItemService;
        }

        [HttpGet]
        public async Task<IEnumerable<CartItem>> GetModels()
        {
            Guid cartId = await GetOrCreateCartIdAsync();

            return await _cartItemService.GetAllByCartAsync(cartId);
        }

        [HttpPost]
        public async Task Create(int[] productIds)
        {
            Guid cartId = await GetOrCreateCartIdAsync();

            await _cartItemService.CreateAsync(cartId, productIds);
        }

        [HttpDelete]
        public async Task Delete(int[] cartItemIds)
        {
            Guid cartId = await GetOrCreateCartIdAsync();

            await _cartItemService.DeleteAsync(cartId, cartItemIds);
        }

        private async Task<Guid> GetOrCreateCartIdAsync()
        {
            Guid cartId;
            
            var cartIdString = HttpContext.Session.GetString(CartIdSessionKey);
            if (cartIdString != null)
            {
                cartId = new Guid(cartIdString);
                var isExisted = await _cartService.CheckAsync(cartId);
                if (isExisted) return cartId;
            }

            cartId = await _cartService.CreateAsync();
            cartIdString = cartId.ToString("D");
            HttpContext.Session.SetString(CartIdSessionKey, cartIdString);
            
            return cartId;
        }
    }
}
