using System.Collections.Generic;
using System.Threading.Tasks;

using CartService.Data.Repository;
using CartService.Domain;

namespace CartService.Services.Impl
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<IReadOnlyCollection<Product>> GetAllAsync()
        {
            return _productRepository.GetAllAsync();
        }
    }
}