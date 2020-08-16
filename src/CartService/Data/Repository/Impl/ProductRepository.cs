using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using CartService.Data.Infrastructure;
using CartService.Domain;

using Dapper.Contrib.Extensions;

namespace CartService.Data.Repository.Impl
{
    internal class ProductRepository : IProductRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public ProductRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<IReadOnlyCollection<Product>> GetAllAsync()
        {
            using (IDbConnection connection = _connectionProvider.CreateConnection())
            {
                IEnumerable<Product> products = await connection.GetAllAsync<Product>();

                return products.ToArray();
            }
        }
    }
}
