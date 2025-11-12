using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    /// <summary>
    /// 資料庫飲品服務
    /// </summary>
    public class DatabaseDrinkService : IDrinkService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DatabaseDrinkService> _logger;

        public DatabaseDrinkService(ApplicationDbContext context, ILogger<DatabaseDrinkService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// 取得所有啟用的分類
        /// </summary>
        public async Task<List<DrinkCategory>> GetCategoriesAsync()
        {
            try
            {
                return await _context.DrinkCategories
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.SortOrder)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "取得分類時發生錯誤");
                return new List<DrinkCategory>();
            }
        }

        /// <summary>
        /// 根據分類取得飲品
        /// </summary>
        public async Task<List<Drink>> GetDrinksByCategoryAsync(int categoryId)
        {
            try
            {
                return await _context.Drinks
                    .Include(d => d.Category)
                    .Where(d => d.CategoryId == categoryId && d.IsAvailable)
                    .OrderBy(d => d.SortOrder)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "取得分類 {CategoryId} 的飲品時發生錯誤", categoryId);
                return new List<Drink>();
            }
        }

        /// <summary>
        /// 取得所有可供應的飲品
        /// </summary>
        public async Task<List<Drink>> GetAllDrinksAsync()
        {
            try
            {
                return await _context.Drinks
                    .Include(d => d.Category)
                    .Where(d => d.IsAvailable)
                    .OrderBy(d => d.CategoryId)
                    .ThenBy(d => d.SortOrder)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "取得所有飲品時發生錯誤");
                return new List<Drink>();
            }
        }

        /// <summary>
        /// 根據ID取得飲品
        /// </summary>
        public async Task<Drink?> GetDrinkByIdAsync(int id)
        {
            try
            {
                return await _context.Drinks
                    .Include(d => d.Category)
                    .FirstOrDefaultAsync(d => d.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "取得飲品 {DrinkId} 時發生錯誤", id);
                return null;
            }
        }
    }
}
