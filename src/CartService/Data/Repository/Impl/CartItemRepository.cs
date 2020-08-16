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
    internal class CartItemRepository : ICartItemRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public CartItemRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<IReadOnlyCollection<CartItem>> GetCartItemsAsync()
        {
            using (IDbConnection connection = _connectionProvider.CreateConnection())
            {
                IEnumerable<CartItem> carts = await connection.GetAllAsync<CartItem>();

                return carts.ToArray();
            }
        }

        public async Task<IReadOnlyList<CartItem>> GetAllByCartAsync(Guid cartId)
        {
            using (IDbConnection connection = _connectionProvider.CreateConnection())
            {
                const string sql = "SELECT * FROM CartItem WHERE CartId = @CartId;";

                IEnumerable<CartItem> result = await connection.QueryAsync<CartItem>(sql, new { CartId = cartId });

                return result.ToArray();
            }
        }

        public async Task<IReadOnlyCollection<CartItem>> GetAllAsync()
        {
            using (IDbConnection connection = _connectionProvider.CreateConnection())
            {
                IEnumerable<CartItem> result = await connection.GetAllAsync<CartItem>();

                return result.ToArray();
            }
        }

        public async Task CreateAsync(IEnumerable<CartItem> cartItems)
        {
            using (IDbConnection connection = _connectionProvider.CreateConnection())
            {
                const string sql = "INSERT INTO CartItem (CartId, ProductId) Values (@CartId, @ProductId);";

                foreach (CartItem cartItem in cartItems)
                {
                    await connection.ExecuteAsync(sql, new { CartId = cartItem.CartId, ProductId = cartItem.ProductId });
                }
            }
        }

        public async Task DeleteAsync(IEnumerable<CartItem> cartItems)
        {
            using (IDbConnection connection = _connectionProvider.CreateConnection())
            {
                foreach (CartItem cartItem in cartItems)
                {
                    await connection.DeleteAsync(cartItem);
                }
            }
        }

        public async Task DeleteAsync(Guid cartId, IEnumerable<int> cartItemIds)
        {
            using (IDbConnection connection = _connectionProvider.CreateConnection())
            {
                const string sql = "DELETE FROM CartItem WHERE CartId = @CartId AND Id = @Id;";
                foreach (var cartItemId in cartItemIds)
                {
                    await connection.ExecuteAsync(sql, new {CartId = cartId, Id = cartItemId});
                }
            }
        }
    }
}
