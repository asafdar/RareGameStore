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

        public async Task<IActionResult> Index(string category)
        {
            if (await _context.Games.CountAsync() == 0)
            {
                List<Game> nes = new List<Game>();
                nes.Add(new Game { Name = "Battletoads", Genre = "Platform", Condition = "Mint", Description = "1991 - This first installment of the Battletoads series is considered to be one of the most difficult video games ever made.", ImagePath = "./images/battletoads.png", Price = 900m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Castlevania", Genre = "Action-Adventure", Condition = "Mint", Description = "1986 - This game, inspired by Bram Stoker's Dracula, is the start to one of the most beloved franchises of all time.", ImagePath = "./images/castlevania.png", Price = 500m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Castlevania II", Genre = "Action-Adventure", Condition = "Mint", Description = "1987 - This sequel finds Simon Belmont as he tries to undo a curse placed on him by Dracula.", ImagePath = "./images/castlevania2.png", Price = 225m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Castlevania III", Genre = "Action-Adventure", Condition = "Mint", Description = "1989 - This third installment of the Castlevania series is highly regarded for its soundtrack and high level of difficulty.", ImagePath = "./images/castlevania3.png", Price = 1200m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Contra", Genre = "Run and Gun", Condition = "Mint", Description = "1988 - Contra was originally released as an arcade game by Konami before being ported to the NES.", ImagePath = "./images/contra.png", Price = 1100m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Donkey Kong", Genre = "Platform", Condition = "Mint", Description = "1986 - This console port of Nintendo's classic arcade game is considered to be one of the most influential games of all time.", ImagePath = "./images/donkeykong.png", Price = 375m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Donkey Kong Jr.", Genre = "Platform", Condition = "Mint", Description = "1986 - This game is in mint condition.", ImagePath = "./images/donkeykongjr.png", Price = 95m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Double Dragon", Genre = "Beat 'em Up", Condition = "Mint", Description = "1986 - This game is in mint condition.", ImagePath = "./images/doubledragon.png", Price = 95m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Final Fantasy", Genre = "Role-Playing Game", Condition = "Mint", Description = "1986 - This game is in mint condition.", ImagePath = "./images/finalfantasy.png", Price = 95m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Galaga", Genre = "Arcade", Condition = "Mint", Description = "1986 - This game is in mint condition.", ImagePath = "./images/galaga.png", Price = 95m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Gradius", Genre = "Arcade", Condition = "Mint", Description = "1986 - This game is in mint condition.", ImagePath = "./images/gradius.png", Price = 95m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Kirby's Adventure", Genre = "Platform", Condition = "Mint", Description = "1986 - This game is in mint condition.", ImagePath = "./images/kirby.png", Price = 95m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "The Legend of Zelda", Genre = "Adventure", Condition = "Mint", Description = "1986 - This game is in mint condition.", ImagePath = "./images/loz.png", Price = 75m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Super Mario Bros.", Genre = "Platform", Condition = "Mint", Description = "1986 - This game is in mint condition.", ImagePath = "./images/mariobrosbox.jpg", Price = 95m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Metroid", Genre = "Action-Adventure", Condition = "Mint", Description = "1986 - This game is in mint condition.", ImagePath = "./images/metroid.jpg", Price = 95m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                nes.Add(new Game { Name = "Mike Tyson's Punch-Out!!", Genre = "Fighting", Condition = "Mint", Description = "1986 - This game is in mint condition.", ImagePath = "./images/tyson.png", Price = 95m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.Platform.Add(new Platform { Name = "NES", Games = nes });

                List<Game> snes = new List<Game>();
                snes.Add(new Game { Name = "Aladdin", Genre = "Platform", Condition = "Good", ImagePath = "/images/aladdin.png", Description = "This classic game based on the film of the same name was released in November 1993.", Price = 200m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Chrono Trigger", Genre = "Role-Playing Game", Condition = "Good", ImagePath = "/images/chronotrigger.png", Description = "Released in 1995, this classic role-playing game is considered by many to be one of the greatest games of all time.", Price = 300m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Contra 3: The Alien Wars", Genre = "Run and Gun", Condition = "Good", ImagePath = "/images/contra3.png", Description = "1992 - This highly acclaimed sequel is considered by many to be the best in the series.", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Donkey Kong Country", Genre = "Platform", Condition = "Good", ImagePath = "/images/dkcountry.png", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Donkey Kong Country 2", Genre = "Platform", Condition = "Good", ImagePath = "/images/dkcountry2.png", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Doom", Genre = "First-person shooter", Condition = "Good", ImagePath = "/images/doom.png", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Dragon's Lair", Genre = "Arcade", Condition = "Good", ImagePath = "/images/dragonslair.png", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Earthbound", Genre = "Role-Playing Game", Condition = "Good", ImagePath = "/images/earthbound.jpg", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "F-Zero", Genre = "Racing", Condition = "Good", ImagePath = "/images/fzero.png", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Final Fantasy II", Genre = "Role-Playing Game", Condition = "Good", ImagePath = "/images/finalfantasy2.png", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Final Fantasy III", Genre = "Role-Playing Game", Condition = "Good", ImagePath = "/images/finalfantasy3.png", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Mega Man X", Genre = "Action-Platform", Condition = "Good", ImagePath = "/images/megamanx.jpg", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Mortal Kombat", Genre = "Fighting", Condition = "Good", ImagePath = "/images/mortalkombat.jpg", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Mortal Kombat II", Genre = "Fighting", Condition = "Good", ImagePath = "/images/mortalkombat2.jpg", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "The Legend of Zelda: A Link To The Past", Genre = "Adventure", Condition = "Good", ImagePath = "/images/alttpbox.jpg", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Secret of Mana", Genre = "Action-RPG", Condition = "Good", ImagePath = "/images/secretofmana.jpg", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Star Fox", Genre = "Shooter", Condition = "Good", ImagePath = "/images/starfox.jpg", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Street Fighter II", Genre = "Fighting", Condition = "Good", ImagePath = "/images/streetfighter2.jpg", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Super Castlevania IV", Genre = "Action-Platform", Condition = "Good", ImagePath = "/images/castlevania4.jpg", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Super Mario Kart", Genre = "Racing", Condition = "Good", ImagePath = "/images/mariokart.png", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Super Mario World", Genre = "Platform", Condition = "Good", ImagePath = "/images/marioworld.jpg", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Super Mario World 2: Yoshi's Island", Genre = "Platform", Condition = "Good", ImagePath = "/images/marioworld2.png", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Super Metroid", Genre = "Action-Adventure", Condition = "Good", ImagePath = "/images/supermetroid.jpg", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                snes.Add(new Game { Name = "Super Star Wars", Genre = "Run and Gun", Condition = "Good", ImagePath = "/images/superstarwars.png", Description = "This game is slightly used", Price = 45m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.Platform.Add(new Platform { Name = "SNES", Games = snes });
                
                await _context.SaveChangesAsync();
            }

            ViewBag.selectedCategory = category;
            List<Platform> model;
            if (string.IsNullOrEmpty(category))
            {
                model = await this._context.Platform.Include(x => x.Games).ToListAsync();
            }
            else
            {
                model = await this._context.Platform.Include(x => x.Games).Where(x => x.Name == category).ToListAsync();
            }
            ViewData["Categories"] = await this._context.Platform.Select(x => x.Name).ToArrayAsync();

            return View(model);
        }

        public async Task<IActionResult> Details (int? id)
        {
            Game model = await _context.Games.FindAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Details (int? id, int quantity)
        {
            GameCart cart = null;
            if (User.Identity.IsAuthenticated)
            {
                //Authenticated path
                var currentUser = await _userManager.GetUserAsync(User);
                cart = await _context.GameCarts.Include(x => x.GameCartProducts).ThenInclude(x => x.Game).FirstOrDefaultAsync(x => x.ApplicationUserID == currentUser.Id);
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
                    
                }
                if (cart == null)
                {
                    cart = new GameCart
                    {
                        DateCreated = DateTime.Now,
                    };
                    _context.GameCarts.Add(cart);
                }
                cart.DateLastModified = DateTime.Now;
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

            await _context.SaveChangesAsync();

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
        
        [HttpPost]
        public async Task<IActionResult> Add(int? id, int quantity)
        {
            GameCart cart = null;
            if (User.Identity.IsAuthenticated)
            {
                //Authenticated path
                var currentUser = await _userManager.GetUserAsync(User);
                cart = await _context.GameCarts.Include(x => x.GameCartProducts).ThenInclude(x => x.Game).FirstOrDefaultAsync(x => x.ApplicationUserID == currentUser.Id);
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

                }
                if (cart == null)
                {
                    cart = new GameCart
                    {
                        DateCreated = DateTime.Now,
                    };
                    _context.GameCarts.Add(cart);
                }
                cart.DateLastModified = DateTime.Now;
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

            await _context.SaveChangesAsync();

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