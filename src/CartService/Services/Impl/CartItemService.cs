using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CartService.Data.Repository;
using CartService.Domain;

namespace CartService.Services.Impl
{
    internal class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartItemService(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public Task<IReadOnlyList<CartItem>> GetAllByCartAsync(Guid cartId)
        {
            return _cartItemRepository.GetAllByCartAsync(cartId);
        }

        public Task<IReadOnlyCollection<CartItem>> GetAllAsync()
        {
            return _cartItemRepository.GetAllAsync();
        }

        public Task CreateAsync(Guid cartId, int[] productIds)
        {
            IEnumerable<CartItem> cartItems = productIds
                .Select(productId => new CartItem
                {
                    CartId = cartId,
                    ProductId = productId
                });

            return _cartItemRepository.CreateAsync(cartItems);
        }

        public Task DeleteAsync(Guid cartId, IEnumerable<int> cartItemIds)
        {
            return _cartItemRepository.DeleteAsync(cartId, cartItemIds);
        }
    }
}