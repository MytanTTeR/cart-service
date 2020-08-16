using System;

using Dapper.Contrib.Extensions;

namespace CartService.Domain
{
    [Table("CartItem")]
    public class CartItem
    {
        public int Id { get; set; }
        public Guid CartId { get; set; }
        public int ProductId { get; set; }
    }
}
