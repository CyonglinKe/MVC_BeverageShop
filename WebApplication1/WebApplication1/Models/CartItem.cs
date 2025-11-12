namespace WebApplication1.Models
{
    public class CartItem
    {
        public int DrinkId { get; set; }
        public string? DrinkName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Temperature { get; set; } // "hot" 或 "cold"
        public string? SweetLevel { get; set; } // 甜度：正常、少糖、半糖、微糖、無糖
        public string? IceLevel { get; set; } // 冰度：正常、少冰、微冰、去冰、溫
        
        public decimal Subtotal => Price * Quantity;
    }
}


