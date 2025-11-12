namespace WebApplication1.Models
{
    public class Order
    {
        public string? CustomerName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? DeliveryMethod { get; set; } // "pickup" 或 "delivery"
        public string? PaymentMethod { get; set; } // "cash" 或 "card"
        public string? Note { get; set; }
        public List<CartItem> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}


