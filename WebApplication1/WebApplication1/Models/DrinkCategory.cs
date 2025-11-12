namespace WebApplication1.Models
{
    /// <summary>
    /// 飲品分類模型
    /// </summary>
    public class DrinkCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? IconClass { get; set; } = string.Empty; // Font Awesome 圖標
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

