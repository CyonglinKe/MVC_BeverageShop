using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private readonly IDrinkService _drinkService;
        private const string CartSessionKey = "ShoppingCart";

        public CartController(IDrinkService drinkService)
        {
            _drinkService = drinkService;
        }

        // 獲取購物車
        private List<CartItem> GetCart()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        // 儲存購物車
        private void SaveCart(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CartSessionKey, cartJson);
        }

        // 查看購物車
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // 添加到購物車 (AJAX)
        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItem item)
        {
            var cart = GetCart();
            
            // 檢查是否已存在相同的商品（包括溫度、甜度、冰度）
            var existingItem = cart.FirstOrDefault(c => 
                c.DrinkId == item.DrinkId && 
                c.Temperature == item.Temperature &&
                c.SweetLevel == item.SweetLevel &&
                c.IceLevel == item.IceLevel);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }

            SaveCart(cart);
            
            return Json(new { 
                success = true, 
                message = "已加入購物車", 
                cartCount = cart.Sum(c => c.Quantity) 
            });
        }

        // 更新數量
        [HttpPost]
        public IActionResult UpdateQuantity(int drinkId, string temperature, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.DrinkId == drinkId && c.Temperature == temperature);
            
            if (item != null)
            {
                if (quantity > 0)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    cart.Remove(item);
                }
                SaveCart(cart);
            }

            return Json(new { success = true, cartCount = cart.Sum(c => c.Quantity) });
        }

        // 移除商品
        [HttpPost]
        public IActionResult RemoveItem(int drinkId, string temperature, string sweetLevel, string iceLevel)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => 
                c.DrinkId == drinkId && 
                c.Temperature == temperature &&
                c.SweetLevel == sweetLevel &&
                c.IceLevel == iceLevel);
            
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return Json(new { success = true, cartCount = cart.Sum(c => c.Quantity) });
        }

        // 清空購物車
        [HttpPost]
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CartSessionKey);
            return Json(new { success = true });
        }

        // 獲取購物車數量
        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = GetCart();
            return Json(new { count = cart.Sum(c => c.Quantity) });
        }

        // 結帳頁面
        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                return RedirectToAction("Index");
            }

            var order = new Order
            {
                Items = cart,
                TotalAmount = cart.Sum(c => c.Subtotal)
            };

            return View(order);
        }

        // 提交訂單
        [HttpPost]
        public IActionResult SubmitOrder(Order order)
        {
            var cart = GetCart();
            
            if (!cart.Any())
            {
                return RedirectToAction("Index");
            }

            // 填充訂單資訊
            order.Items = cart;
            order.TotalAmount = cart.Sum(c => c.Subtotal);
            order.OrderDate = DateTime.Now;

            // 這裡可以保存到資料庫
            // TODO: 保存訂單到資料庫

            // 清空購物車
            HttpContext.Session.Remove(CartSessionKey);

            // 暫時將訂單資訊存到 TempData 用於顯示
            TempData["OrderInfo"] = JsonSerializer.Serialize(order);

            return RedirectToAction("OrderComplete");
        }

        // 訂單完成頁面
        public IActionResult OrderComplete()
        {
            var orderJson = TempData["OrderInfo"] as string;
            if (string.IsNullOrEmpty(orderJson))
            {
                return RedirectToAction("Index", "Home");
            }

            var order = JsonSerializer.Deserialize<Order>(orderJson);
            return View(order);
        }
    }
}


