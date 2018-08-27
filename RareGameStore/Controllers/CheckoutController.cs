using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Braintree;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RareGameStore.Data;
using RareGameStore.Models;
using RareGameStore.Services;
using SmartyStreets.USStreetApi;

namespace RareGameStore.Controllers
{
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IEmailSender _emailSender;
        private IBraintreeGateway _braintreeGateway;
        private Client _client;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender, IBraintreeGateway braintreeGateway, Client client)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _braintreeGateway = braintreeGateway;
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            CheckoutModel model = new CheckoutModel();
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                model.Email = currentUser.Email;
            }

            ViewBag.ClientAuthorization = await _braintreeGateway.ClientToken.GenerateAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CheckoutModel model, string nonce)
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

                await _braintreeGateway.Transaction.SaleAsync(new TransactionRequest
                {
                    Amount = (decimal)order.GameOrderProducts.Sum(x => x.Quantity * x.ProductPrice),    //You can also do 1m here
                    CreditCard = new TransactionCreditCardRequest
                    {
                        CardholderName = "Test Cardholder",
                        CVV = "123",
                        ExpirationMonth = DateTime.Now.AddMonths(1).ToString("MM"),
                        ExpirationYear = DateTime.Now.AddMonths(1).ToString("yyyy"),
                        Number = "4111111111111111"
                    }
                });

                var result = await _braintreeGateway.Transaction.SaleAsync(new TransactionRequest
                {
                    Amount = (decimal)order.GameOrderProducts.Sum(x => x.Quantity * x.ProductPrice),    //You can also do 1m here
                    PaymentMethodNonce = nonce
                });

                await _emailSender.SendEmailAsync(model.Email, "Your order " + order.ID, "Thanks for ordering!  You bought : " + String.Join(",", order.GameOrderProducts.Select(x => x.ProductName)));

                //TODO: Save this information to the database so we can ship the order
                return RedirectToAction("Index", "Receipt", new { id = order.ID });
            }
            //TODO: we have an error!  Redisplay the form!
            return View();
        }

        [HttpPost]
        public IActionResult ValidateAddress([FromBody]Lookup lookup)
        {
            try
            {
                _client.Send(lookup);
                if (lookup.Result.Any())
                {
                    return Json(lookup.Result.First());
                }
                else
                {
                    return BadRequest("No matches found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}