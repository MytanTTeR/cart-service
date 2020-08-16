using System.Data;

namespace CartService.Data.Infrastructure
{
    internal interface IConnectionProvider
    {
        IDbConnection CreateConnection();
    }
}
