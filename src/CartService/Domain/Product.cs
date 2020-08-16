using System.ComponentModel.DataAnnotations.Schema;

namespace CartService.Domain
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public bool ForBonusPoints { get; set; }
    }
}
