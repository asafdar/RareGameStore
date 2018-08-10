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
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            CheckoutModel model = new CheckoutModel();
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = _userManager.GetUserAsync(User).Result;
                model.Email = currentUser.Email;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CheckoutModel model)
        {
            if (ModelState.IsValid)
            {
                GameOrder order = new GameOrder
                {
                    City = model.City,
                    State = model.State,
                    Email = model.Email,
                    StreetAddress = model.StreetAddress,
                    ZipCode = model.ZipCode,
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now
                };
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
                foreach (var cartItem in cart.GameCartProducts)
                {
                    order.GameOrderProducts.Add(new GameOrderProduct
                    {
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now,
                        Quantity = cartItem.Quantity ?? 1,
                        ProductID = cartItem.GameID,
                        ProductDescription = cartItem.Game.Description,
                        ProductName = cartItem.Game.Name,
                        ProductPrice = cartItem.Game.Price ?? 0
                    });
                }

                _context.GameCartProducts.RemoveRange(cart.GameCartProducts);
                _context.GameCarts.Remove(cart);

                if (Request.Cookies.ContainsKey("cart_id"))
                {
                    Response.Cookies.Delete("cart_id");
                }

                _context.GameOrders.Add(order);
                _context.SaveChanges();
                //TODO: Save this information to the database so we can ship the order
                return RedirectToAction("Index", "Receipt", new { id = order.ID });
            }
            //TODO: we have an error!  Redisplay the form!
            return View();
        }
    }
}