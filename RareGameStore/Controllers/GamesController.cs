using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RareGameStore.Data;
using RareGameStore.Models;

namespace RareGameStore.Controllers
{
    public class GamesController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public GamesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public IActionResult Index(string category)
        {
            if (_context.Games.Count() == 0)
            {
                List<Game> nes = new List<Game>();
                nes.Add(new Game {Name = "Super Mario Bros.", Genre = "Platform", Condition = "Mint", Description = "1986 - This game is in mint condition.", ImagePath = "./images/mariobros.jpg", Price = 95m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now});
                //Repeat this many times!
                _context.Platform.Add(new Platform { Name = "NES", Games = nes });

                List<Game> snes = new List<Game>();
                snes.Add(new Game { Name = "A Link To The Past", Genre = "Adventure", Condition = "Good", ImagePath = "/images/alttp.jpg", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.Platform.Add(new Platform { Name = "SNES", Games = snes });
                
                _context.SaveChanges();
            }

            ViewBag.selectedCategory = category;
            List<Platform> model;
            if (string.IsNullOrEmpty(category))
            {
                model = this._context.Platform.Include(x => x.Games).ToList();
            }
            else
            {
                model = this._context.Platform.Include(x => x.Games).Where(x => x.Name == category).ToList();
            }
            ViewData["Categories"] = this._context.Platform.Select(x => x.Name).ToArray();

            return View(model);
        }

        public IActionResult Details(int? id)
        {
            Game model = _context.Games.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Details(int? id, int quantity, string color)
        {
            GameCart cart = null;
            if (User.Identity.IsAuthenticated)
            {
                //Authenticated path
                var currentUser = _userManager.GetUserAsync(User).Result;
                cart = _context.GameCarts.Include(x => x.GameCartProducts).ThenInclude(x => x.Game).FirstOrDefault(x => x.ApplicationUserID == currentUser.Id);
                if (cart == null)
                {
                    cart = new GameCart();
                    cart.ApplicationUserID = currentUser.Id;
                    cart.DateCreated = DateTime.Now;
                    cart.DateLastModified = DateTime.Now;
                    _context.GameCarts.Add(cart);
                }

            }
            else
            {
                if (Request.Cookies.ContainsKey("cart_id"))
                {
                    int existingCartId = int.Parse(Request.Cookies["cart_id"]);
                    cart = _context.GameCarts.Include(x => x.GameCartProducts).FirstOrDefault(x => x.ID == existingCartId);
                    cart.DateLastModified = DateTime.Now;
                }
                if (cart == null)
                {
                    cart = new GameCart
                    {
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now
                    };
                    _context.GameCarts.Add(cart);
                }
            }
            
            GameCartProduct product = cart.GameCartProducts.FirstOrDefault(x => x.GameID == id);
            if (product == null)
            {
                product = new GameCartProduct
                {
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now,
                    GameID = id ?? 0,
                    Quantity = 0
                };
                cart.GameCartProducts.Add(product);
            }
            product.Quantity += quantity;
            product.DateLastModified = DateTime.Now;

            _context.SaveChanges();

            if (!User.Identity.IsAuthenticated)
            {
                //At the end of this page, always set the cookie.  This might just overwrite the old cookie!
                Response.Cookies.Append("cart_id", cart.ID.ToString(), new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1)
                });
            }

            return RedirectToAction("Index", "Cart");
        }
    }
}