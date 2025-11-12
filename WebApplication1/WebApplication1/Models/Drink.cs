namespace WebApplication1.Models
{
    /// <summary>
    /// 飲品模型
    /// </summary>
    public class Drink
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public DrinkCategory? Category { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public bool IsHot { get; set; } = true; // 是否可做熱飲
        public bool IsCold { get; set; } = true; // 是否可做冷飲
        public bool IsSeasonal { get; set; } = false; // 是否為季節限定
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}

