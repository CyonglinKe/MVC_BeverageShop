using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class MenuController : Controller
    {
        private readonly IDrinkService _drinkService;

        public MenuController(IDrinkService drinkService)
        {
            _drinkService = drinkService;
        }

        /// <summary>
        /// 菜單首頁
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var categories = await _drinkService.GetCategoriesAsync();
            var drinks = await _drinkService.GetAllDrinksAsync();
            
            var viewModel = new MenuViewModel
            {
                Categories = categories,
                AllDrinks = drinks
            };

            return View(viewModel);
        }

        /// <summary>
        /// 根據分類取得飲品
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDrinksByCategory(int categoryId)
        {
            var drinks = await _drinkService.GetDrinksByCategoryAsync(categoryId);
            return Json(drinks);
        }

        /// <summary>
        /// 飲品詳情
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            var drink = await _drinkService.GetDrinkByIdAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }
    }

    /// <summary>
    /// 菜單頁面視圖模型
    /// </summary>
    public class MenuViewModel
    {
        public List<DrinkCategory> Categories { get; set; } = new();
        public List<Drink> AllDrinks { get; set; } = new();
        public int? SelectedCategoryId { get; set; }
    }
}

