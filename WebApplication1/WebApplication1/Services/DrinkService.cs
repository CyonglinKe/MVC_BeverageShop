using WebApplication1.Models;

namespace WebApplication1.Services
{
    /// <summary>
    /// 飲品資料服務介面
    /// </summary>
    public interface IDrinkService
    {
        Task<List<DrinkCategory>> GetCategoriesAsync();
        Task<List<Drink>> GetDrinksByCategoryAsync(int categoryId);
        Task<List<Drink>> GetAllDrinksAsync();
        Task<Drink?> GetDrinkByIdAsync(int id);
    }

    /// <summary>
    /// 假資料飲品服務
    /// </summary>
    public class MockDrinkService : IDrinkService
    {
        private readonly List<DrinkCategory> _categories;
        private readonly List<Drink> _drinks;

        public MockDrinkService()
        {
            _categories = GetMockCategories();
            _drinks = GetMockDrinks();
        }

        public Task<List<DrinkCategory>> GetCategoriesAsync()
        {
            return Task.FromResult(_categories.Where(c => c.IsActive).OrderBy(c => c.SortOrder).ToList());
        }

        public Task<List<Drink>> GetDrinksByCategoryAsync(int categoryId)
        {
            return Task.FromResult(_drinks.Where(d => d.CategoryId == categoryId && d.IsAvailable).OrderBy(d => d.SortOrder).ToList());
        }

        public Task<List<Drink>> GetAllDrinksAsync()
        {
            return Task.FromResult(_drinks.Where(d => d.IsAvailable).OrderBy(d => d.CategoryId).ThenBy(d => d.SortOrder).ToList());
        }

        public Task<Drink?> GetDrinkByIdAsync(int id)
        {
            return Task.FromResult(_drinks.FirstOrDefault(d => d.Id == id));
        }

        private List<DrinkCategory> GetMockCategories()
        {
            return new List<DrinkCategory>
            {
                new() { Id = 1, Name = "經典茶類", Description = "傳統茶飲，經典口味", IconClass = "fas fa-leaf", SortOrder = 1 },
                new() { Id = 2, Name = "水果茶類", Description = "新鮮水果與茶的完美結合", IconClass = "fas fa-apple-alt", SortOrder = 2 },
                new() { Id = 3, Name = "鮮奶茶類", Description = "香濃鮮奶與茶的搭配", IconClass = "fas fa-coffee", SortOrder = 3 },
                new() { Id = 4, Name = "奶蓋茶類", Description = "濃郁奶蓋，層次豐富", IconClass = "fas fa-cloud", SortOrder = 4 },
                new() { Id = 5, Name = "特色飲品", Description = "獨特配方，創意無限", IconClass = "fas fa-star", SortOrder = 5 },
                new() { Id = 6, Name = "咖啡飲品", Description = "香醇咖啡，提神醒腦", IconClass = "fas fa-mug-hot", SortOrder = 6 },
                new() { Id = 7, Name = "季節限定", Description = "限時推出，錯過可惜", IconClass = "fas fa-snowflake", SortOrder = 7 }
            };
        }

        private List<Drink> GetMockDrinks()
        {
            return new List<Drink>
            {
                // 經典茶類
                new() { Id = 1, Name = "珍珠奶茶", Description = "Q彈珍珠搭配香濃奶茶", Price = 55, CategoryId = 1, SortOrder = 1 },
                new() { Id = 2, Name = "四季春", Description = "清香淡雅，回甘甘甜", Price = 35, CategoryId = 1, SortOrder = 2 },
                new() { Id = 3, Name = "紅茶", Description = "經典紅茶，香醇濃郁", Price = 30, CategoryId = 1, SortOrder = 3 },
                new() { Id = 4, Name = "烏龍茶", Description = "台灣烏龍，茶香四溢", Price = 35, CategoryId = 1, SortOrder = 4 },
                new() { Id = 5, Name = "鐵觀音", Description = "濃郁鐵觀音，回甘持久", Price = 40, CategoryId = 1, SortOrder = 5 },
                new() { Id = 6, Name = "綠茶", Description = "清新綠茶，健康首選", Price = 30, CategoryId = 1, SortOrder = 6 },
                new() { Id = 7, Name = "青茶", Description = "清香青茶，淡雅怡人", Price = 35, CategoryId = 1, SortOrder = 7 },
                new() { Id = 8, Name = "仙女紅茶", Description = "傳說中的仙女紅茶", Price = 45, CategoryId = 1, SortOrder = 8 },

                // 水果茶類
                new() { Id = 9, Name = "檸檬綠茶", Description = "新鮮檸檬搭配清香綠茶", Price = 50, CategoryId = 2, SortOrder = 1 },
                new() { Id = 10, Name = "百香綠茶", Description = "酸甜百香果與綠茶", Price = 55, CategoryId = 2, SortOrder = 2 },
                new() { Id = 11, Name = "芒果綠茶", Description = "香甜芒果與清新綠茶", Price = 60, CategoryId = 2, SortOrder = 3 },
                new() { Id = 12, Name = "柳丁綠茶", Description = "鮮甜柳丁與綠茶", Price = 50, CategoryId = 2, SortOrder = 4 },
                new() { Id = 13, Name = "荔枝紅茶", Description = "香甜荔枝與濃郁紅茶", Price = 55, CategoryId = 2, SortOrder = 5 },
                new() { Id = 14, Name = "莓果康普紅茶", Description = "綜合莓果與康普茶", Price = 65, CategoryId = 2, SortOrder = 6 },
                new() { Id = 15, Name = "熱帶水果氣泡", Description = "熱帶水果與氣泡水", Price = 60, CategoryId = 2, SortOrder = 7 },
                new() { Id = 16, Name = "香柚綠茶", Description = "清香柚子與綠茶", Price = 55, CategoryId = 2, SortOrder = 8 },
                new() { Id = 17, Name = "蘋果芒芒綠", Description = "蘋果芒果與綠茶", Price = 60, CategoryId = 2, SortOrder = 9 },

                // 鮮奶茶類
                new() { Id = 18, Name = "芋頭鮮奶", Description = "香濃芋頭與鮮奶", Price = 65, CategoryId = 3, SortOrder = 1 },
                new() { Id = 19, Name = "黑糖鮮奶", Description = "濃郁黑糖與鮮奶", Price = 60, CategoryId = 3, SortOrder = 2 },
                new() { Id = 20, Name = "豆漿紅茶", Description = "香濃豆漿與紅茶", Price = 50, CategoryId = 3, SortOrder = 3 },
                new() { Id = 21, Name = "鮮奶拿鐵", Description = "香濃鮮奶拿鐵", Price = 55, CategoryId = 3, SortOrder = 4 },

                // 奶蓋茶類
                new() { Id = 22, Name = "黑朵奶蓋四季春", Description = "濃郁奶蓋與四季春", Price = 60, CategoryId = 4, SortOrder = 1 },
                new() { Id = 23, Name = "黑朵奶蓋鐵觀音", Description = "濃郁奶蓋與鐵觀音", Price = 65, CategoryId = 4, SortOrder = 2 },
                new() { Id = 24, Name = "海鹽奶蓋", Description = "鹹甜海鹽奶蓋", Price = 55, CategoryId = 4, SortOrder = 3 },
                new() { Id = 25, Name = "芝士奶蓋", Description = "濃郁芝士奶蓋", Price = 60, CategoryId = 4, SortOrder = 4 },

                // 特色飲品
                new() { Id = 26, Name = "咖啡凍拿鐵", Description = "Q彈咖啡凍與拿鐵", Price = 65, CategoryId = 5, SortOrder = 1 },
                new() { Id = 27, Name = "布丁奶茶", Description = "滑嫩布丁與奶茶", Price = 55, CategoryId = 5, SortOrder = 2 },
                new() { Id = 28, Name = "燒仙草", Description = "傳統燒仙草", Price = 45, CategoryId = 5, SortOrder = 3 },
                new() { Id = 29, Name = "愛玉", Description = "清涼愛玉", Price = 40, CategoryId = 5, SortOrder = 4 },
                new() { Id = 30, Name = "粉粿", Description = "Q彈粉粿", Price = 35, CategoryId = 5, SortOrder = 5 },
                new() { Id = 31, Name = "寒天晶球", Description = "晶瑩剔透寒天", Price = 40, CategoryId = 5, SortOrder = 6 },
                new() { Id = 32, Name = "檸檬冬瓜", Description = "酸甜檸檬與冬瓜", Price = 45, CategoryId = 5, SortOrder = 7 },
                new() { Id = 33, Name = "冬瓜檸檬", Description = "清香冬瓜與檸檬", Price = 45, CategoryId = 5, SortOrder = 8 },
                new() { Id = 34, Name = "養樂多系列", Description = "酸甜養樂多", Price = 50, CategoryId = 5, SortOrder = 9 },

                // 咖啡飲品
                new() { Id = 35, Name = "美式咖啡", Description = "香醇美式咖啡", Price = 50, CategoryId = 6, SortOrder = 1 },
                new() { Id = 36, Name = "拿鐵咖啡", Description = "香濃拿鐵咖啡", Price = 60, CategoryId = 6, SortOrder = 2 },
                new() { Id = 37, Name = "卡布奇諾", Description = "經典卡布奇諾", Price = 60, CategoryId = 6, SortOrder = 3 },
                new() { Id = 38, Name = "濃情巧克力", Description = "濃郁巧克力飲品", Price = 55, CategoryId = 6, SortOrder = 4 },

                // 季節限定
                new() { Id = 39, Name = "芒果季節限定", Description = "香甜芒果限定飲品", Price = 70, CategoryId = 7, SortOrder = 1, IsSeasonal = true },
                new() { Id = 40, Name = "草莓季節限定", Description = "酸甜草莓限定飲品", Price = 70, CategoryId = 7, SortOrder = 2, IsSeasonal = true }
            };
        }
    }
}

