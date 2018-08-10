using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RareGameStore.Data;
using RareGameStore.Models;

namespace RareGameStore.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            GameCart cart = null;
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = _userManager.GetUserAsync(User).Result;
                cart = _context.GameCarts.Include(x => x.GameCartProducts).ThenInclude(x => x.Game).Single(x => x.ApplicationUserID == currentUser.Id);
            }
            else if (Request.Cookies.ContainsKey("cart_id"))
            {
                int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                cart = _context.GameCarts.Include(x => x.GameCartProducts).ThenInclude(x => x.Game).FirstOrDefault(x => x.ID == existingCartID);
            }
            else
            {
                cart = new GameCart();
            }

            return View(cart);
        }

        public IActionResult Remove(int id)
        {
            return RedirectToAction("Index");
        }
    }
}