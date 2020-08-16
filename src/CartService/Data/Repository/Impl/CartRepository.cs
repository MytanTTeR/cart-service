using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using CartService.Data.Infrastructure;
using CartService.Domain;

using Dapper;
using Dapper.Contrib.Extensions;

namespace CartService.Data.Repository.Impl
{
    internal class CartRepository : ICartRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public CartRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<Cart> GetAsync(Guid cartId)
        {
            using (IDbConnection connection = _connectionProvider.CreateConnection())
            {
                const string sql = "SELECT TOP 1 * FROM Cart WHERE Id = @Id;";

                return await connection.QueryFirstAsync<Cart>(sql, new { Id = cartId });
            }
        }

        public async Task<IReadOnlyCollection<Cart>> GetAllAsync()
        {
            using (IDbConnection connection = _connectionProvider.CreateConnection())
            {
                IEnumerable<Cart> carts = await connection.GetAllAsync<Cart>();

                return carts.ToArray();
            }
        }

        public async Task<IReadOnlyCollection<Cart>> GetEstimatedAsync(DateTime estimatedDate)
        {
            using (IDbConnection connection = _connectionProvider.CreateConnection())
            {
                const string sql = "SELECT * FROM Cart WHERE CreatedDate < @EstimatedCreatedDate;";

                IEnumerable<Cart> carts = await connection.QueryAsync<Cart>(sql, new { EstimatedCreatedDate = estimatedDate });

                return carts.ToArray();
            }
        }

        public async Task DeleteAsync(IEnumerable<Cart> carts)
        {
            using (IDbConnection connection = _connectionProvider.CreateConnection())
            {
                const string sql = "DELETE FROM Cart WHERE Id = @Id;";

                foreach (Cart cart in carts)
                {
                    await connection.ExecuteAsync(sql, new {Id = cart.Id});
                }
            }
        }

        public async Task<Guid> CreateAsync()
        {
            Guid cartId = Guid.NewGuid();
            using (IDbConnection connection = _connectionProvider.CreateConnection())
            {
                const string sql = "INSERT INTO Cart (Id, CreatedDate) Values (@Id, @CreatedDate);";
                await connection.ExecuteAsync(sql, new { Id = cartId, CreatedDate = DateTime.UtcNow });
            }
            return cartId;
        }
    }
}
