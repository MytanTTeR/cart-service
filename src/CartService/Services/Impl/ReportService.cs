using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CartService.Domain;

namespace CartService.Services.Impl
{
    internal class ReportService : IReportService
    {
        private readonly IProductService _productService;
        private readonly ICartItemService _cartItemService;
        private readonly ICartService _cartService;

        public ReportService(
            IProductService productService,
            ICartItemService cartItemService, 
            ICartService cartService)
        {
            _productService = productService;
            _cartItemService = cartItemService;
            _cartService = cartService;
        }

        public async Task SaveReportAsync()
        {
            IReadOnlyCollection<Cart> carts = await _cartService.GetAllAsync();
            IReadOnlyCollection<CartItem> cartItems = await _cartItemService.GetAllAsync();
            IReadOnlyCollection<Product> products = await _productService.GetAllAsync();

            var sb = new StringBuilder();

            WriteTotalCarts(carts, sb);
            WriteCartsWithBonus(sb, carts, cartItems, products);
            WriteCartsToExpire(sb, carts);
            WriteCartsCostAverage(sb, carts, cartItems, products);

            var reportText = sb.ToString();

            await SaveReportToFileAsync(reportText);
        }

        private static Task SaveReportToFileAsync(string reportText)
        {
            var reportName = $"carts_report_{DateTime.UtcNow:dd_MM_yyyy}.txt";
            var reportsPath = Path.Combine(Environment.CurrentDirectory, "reports");

            Directory.CreateDirectory(reportsPath);

            var reportPath = Path.Combine(reportsPath, reportName);
            return File.WriteAllTextAsync(reportPath, reportText, Encoding.UTF8);
        }

        private static void WriteTotalCarts(IEnumerable<Cart> carts, StringBuilder sb)
        {
            var cartsTotal = carts.Count();
            sb.AppendLine($"Всего корзин: {cartsTotal}");
        }

        private static void WriteCartsWithBonus(StringBuilder sb, IEnumerable<Cart> carts, IReadOnlyCollection<CartItem> cartItems, IEnumerable<Product> products)
        {
            var cartsWithBonus = carts.Count(cart =>
                cartItems.Any(cartItem =>
                    cartItem.CartId == cart.Id && products.Any(product => product.Id == cartItem.Id && product.ForBonusPoints)
                )
            );
            sb.AppendLine($"Корзин с баллами: {cartsWithBonus}");
        }

        private static void WriteCartsToExpire(StringBuilder sb, IEnumerable<Cart> carts)
        {
            DateTime utcNow = DateTime.UtcNow;
            byte[] cartsToExpire = carts
                .Select(x => x.CreatedDate - utcNow)
                .Select(x => (byte)(30 - x.TotalDays))
                .ToArray();

            var daysToExpire = new byte[] { 10, 20, 30 };
            foreach (var days in daysToExpire)
            {
                var cartsToExpireCount = cartsToExpire.Count(x => x <= days);
                sb.AppendLine($"Через {days} дней истечёт корзин: {cartsToExpireCount}");
            }
        }

        private static void WriteCartsCostAverage(StringBuilder sb, IEnumerable<Cart> carts, IReadOnlyCollection<CartItem> cartItems, IEnumerable<Product> products)
        {
            decimal[] cartCosts = carts.Select(cart =>
                cartItems
                    .Where(cartItem => cartItem.CartId == cart.Id)
                    .Sum(cartItem =>
                        products.First(product => product.Id == cartItem.ProductId).Cost
                    )
            ).ToArray();

            var cartCostsAverage = cartCosts.Sum() / cartCosts.Count();
            sb.AppendLine($"Средний чек корзины: {cartCostsAverage}");
        }
    }
}