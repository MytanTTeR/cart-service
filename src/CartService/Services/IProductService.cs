using System.Collections.Generic;
using System.Threading.Tasks;

using CartService.Domain;

namespace CartService.Services
{
    public interface IProductService
    {
        Task<IReadOnlyCollection<Product>> GetAllAsync();
    }
}
