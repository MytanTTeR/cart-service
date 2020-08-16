using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CartService.Domain;

namespace CartService.Data.Repository
{
    public interface IProductRepository
    {
        Task<IReadOnlyCollection<Product>> GetAllAsync();
    }
}
