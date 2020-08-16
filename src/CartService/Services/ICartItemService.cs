using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CartService.Domain;

namespace CartService.Services
{
    public interface ICartItemService
    {
        Task<IReadOnlyList<CartItem>> GetAllByCartAsync(Guid cartId);
        Task CreateAsync(Guid cartId, int[] productIds);
        Task DeleteAsync(Guid cartId, IEnumerable<int> cartItemIds);
        Task<IReadOnlyCollection<CartItem>> GetAllAsync();
    }
}
