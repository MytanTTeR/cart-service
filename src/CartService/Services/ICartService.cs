using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CartService.Domain;

namespace CartService.Services
{
    public interface ICartService
    {
        Task<Guid> CreateAsync();
        Task CloseEstimatedAsync();
        Task<bool> CheckAsync(Guid cartId);
        Task<IReadOnlyCollection<Cart>> GetAllAsync();
    }
}
