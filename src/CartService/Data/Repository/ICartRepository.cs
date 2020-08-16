using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CartService.Domain;

namespace CartService.Data.Repository
{
    public interface ICartRepository
    {
        Task<Cart> GetAsync(Guid cartId);
        Task<IReadOnlyCollection<Cart>> GetAllAsync();
        Task<IReadOnlyCollection<Cart>> GetEstimatedAsync(DateTime estimatedDate);
        Task<Guid> CreateAsync();
        Task DeleteAsync(IEnumerable<Cart> carts);
    }
}
