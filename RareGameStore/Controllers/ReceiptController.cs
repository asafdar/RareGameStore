using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RareGameStore.Data;
using RareGameStore.Models;

namespace RareGameStore.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceiptController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Receipt/Details/5
        public async Task<IActionResult> Index(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameOrder = await _context.GameOrders.Include(x => x.GameOrderProducts)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (gameOrder == null)
            {
                return NotFound();
            }

            return View(gameOrder);
        }
    }
}
