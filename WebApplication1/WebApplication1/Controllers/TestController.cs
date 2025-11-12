using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using System.Text;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 測試資料庫連接
        /// </summary>
        public async Task<IActionResult> Database()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<h1>資料庫連接測試</h1>");
            sb.AppendLine("<hr>");

            try
            {
                // 測試連接
                await _context.Database.OpenConnectionAsync();
                sb.AppendLine("<p style='color:green;'>✅ 資料庫連接成功！</p>");
                await _context.Database.CloseConnectionAsync();

                // 檢查分類數量
                var categoryCount = await _context.DrinkCategories.CountAsync();
                sb.AppendLine($"<p>📊 分類數量: <strong>{categoryCount}</strong></p>");

                // 檢查飲品數量
                var drinkCount = await _context.Drinks.CountAsync();
                sb.AppendLine($"<p>🍹 飲品數量: <strong>{drinkCount}</strong></p>");

                if (categoryCount == 0 && drinkCount == 0)
                {
                    sb.AppendLine("<hr>");
                    sb.AppendLine("<h2 style='color:orange;'>⚠️ 資料庫中沒有資料！</h2>");
                    sb.AppendLine("<p>請在 SQL Server Management Studio 中執行 <code>database_setup.sql</code> 腳本來插入資料。</p>");
                    sb.AppendLine("<p>腳本位置: <code>WebApplication1/database_setup.sql</code></p>");
                }
                else
                {
                    sb.AppendLine("<hr>");
                    sb.AppendLine("<h2>分類列表：</h2>");
                    var categories = await _context.DrinkCategories.OrderBy(c => c.SortOrder).ToListAsync();
                    sb.AppendLine("<ul>");
                    foreach (var cat in categories)
                    {
                        sb.AppendLine($"<li>{cat.Name} (ID: {cat.Id}, 啟用: {cat.IsActive})</li>");
                    }
                    sb.AppendLine("</ul>");

                    sb.AppendLine("<h2>前 10 款飲品：</h2>");
                    var drinks = await _context.Drinks.Take(10).ToListAsync();
                    sb.AppendLine("<ul>");
                    foreach (var drink in drinks)
                    {
                        sb.AppendLine($"<li>{drink.Name} - NT$ {drink.Price} (CategoryId: {drink.CategoryId}, 可供應: {drink.IsAvailable})</li>");
                    }
                    sb.AppendLine("</ul>");
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine($"<p style='color:red;'>❌ 錯誤: {ex.Message}</p>");
                sb.AppendLine($"<pre>{ex.StackTrace}</pre>");
            }

            sb.AppendLine("<hr>");
            sb.AppendLine("<p><a href='/Menu'>返回菜單頁面</a></p>");

            return Content(sb.ToString(), "text/html", Encoding.UTF8);
        }
    }
}


