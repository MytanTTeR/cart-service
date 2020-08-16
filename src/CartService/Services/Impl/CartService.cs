using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CartService.Data.Repository;
using CartService.Domain;

namespace CartService.Services.Impl
{
    internal class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Task<Guid> CreateAsync()
        {
            return _cartRepository.CreateAsync();
        }

        public Task<IReadOnlyCollection<Cart>> GetAllAsync()
        {
            return _cartRepository.GetAllAsync();
        }

        public async Task CloseEstimatedAsync()
        {
            DateTime estimatedDate = DateTime.UtcNow.AddDays(-30);
            IReadOnlyCollection<Cart> estimatedCarts = await _cartRepository.GetEstimatedAsync(estimatedDate);
            await _cartRepository.DeleteAsync(estimatedCarts);
        }

        public async Task<bool> CheckAsync(Guid cartId)
        {
            Cart cart = await _cartRepository.GetAsync(cartId);
            return cart != null;
        }
    }
}