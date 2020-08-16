using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CartService.Domain;

namespace CartService.Data.Repository
{
    public interface ICartItemRepository
    {
        Task<IReadOnlyCollection<CartItem>> GetCartItemsAsync();
        Task<IReadOnlyList<CartItem>> GetAllByCartAsync(Guid cartId);
        Task<IReadOnlyCollection<CartItem>> GetAllAsync();

        Task CreateAsync(IEnumerable<CartItem> cartItems);
        Task DeleteAsync(IEnumerable<CartItem> cartItems);
        Task DeleteAsync(Guid cartId, IEnumerable<int> cartItemIds);
    }
}
