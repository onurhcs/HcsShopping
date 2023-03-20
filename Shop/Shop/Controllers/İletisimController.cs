using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    public class İletisimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public İletisimController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        // GET: İletisim/Create
        public IActionResult Index()
        {
           
            return View();
           
        }

        // POST: İletisim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,AdSoyad,TelefonNo,EMail,Konu,MesajMetni,Tarih,Okundu,OkunmaTarihi")] İletisim İletisim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(İletisim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(İletisim);
        }

        
        private bool İletisimExists(int id)
        {
          return (_context.Iletisimler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
