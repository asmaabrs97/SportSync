using Microsoft.AspNetCore.Mvc;
using SportSync.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SportSync.Extensions;

namespace SportSync.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "CartItems";

        public IActionResult Index()
        {
            var cartItems = GetCartItems();
            return View(cartItems);
        }

        public IActionResult AddToCart(string discipline, string session)
        {
            var cartItems = GetCartItems();
            
            cartItems.Add(new CartItem 
            { 
                Id = cartItems.Count > 0 ? cartItems.Max(i => i.Id) + 1 : 1,
                Discipline = discipline,
                Session = session,
                Price = 365.00m, // This should come from your session pricing data
                DateAdded = System.DateTime.Now,
                UserId = User.Identity?.Name ?? "anonymous"
            });

            SaveCartItems(cartItems);
            
            TempData["Message"] = "Session added to cart successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cartItems = GetCartItems();
            var itemToRemove = cartItems.FirstOrDefault(i => i.Id == id);
            
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
                SaveCartItems(cartItems);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cartItems = GetCartItems();
            if (!cartItems.Any())
            {
                return RedirectToAction("Index");
            }
            return View(cartItems);
        }

        private List<CartItem> GetCartItems()
        {
            return HttpContext.Session.Get<List<CartItem>>(CartSessionKey) ?? new List<CartItem>();
        }

        private void SaveCartItems(List<CartItem> cartItems)
        {
            HttpContext.Session.Set(CartSessionKey, cartItems);
        }
    }
}
