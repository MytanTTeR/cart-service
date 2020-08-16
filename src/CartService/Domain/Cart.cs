using System;

using Dapper.Contrib.Extensions;

namespace CartService.Domain
{
    [Table("Cart")]
    public class Cart
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
