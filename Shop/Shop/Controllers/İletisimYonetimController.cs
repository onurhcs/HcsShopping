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
    public class İletisimYonetimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public İletisimYonetimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: İletisimYonetim
        public async Task<IActionResult> Index()
        {
              return _context.Iletisimler != null ? 
                          View(await _context.Iletisimler.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Iletisimler'  is null.");
        }

        // GET: İletisimYonetim/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Iletisimler == null)
            {
                return NotFound();
            }

            var İletisim = await _context.Iletisimler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (İletisim == null)
            {
                return NotFound();
            }

            return View(İletisim);
        }

    
        // GET: İletisimYonetim/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Iletisimler == null)
            {
                return NotFound();
            }

            var İletisim = await _context.Iletisimler.FindAsync(id);
            if (İletisim == null)
            {
                return NotFound();
            }
            return View(İletisim);
        }

        // POST: İletisimYonetim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AdSoyad,TelefonNo,EMail,Konu,MesajMetni,Tarih,Okundu,OkunmaTarihi")] İletisim İletisim)
        {
            if (id != İletisim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(İletisim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!İletisimExists(İletisim.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(İletisim);
        }

        // GET: İletisimYonetim/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Iletisimler == null)
            {
                return NotFound();
            }

            var İletisim = await _context.Iletisimler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (İletisim == null)
            {
                return NotFound();
            }

            return View(İletisim);
        }

        // POST: İletisimYonetim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Iletisimler == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Iletisimler'  is null.");
            }
            var İletisim = await _context.Iletisimler.FindAsync(id);
            if (İletisim != null)
            {
                _context.Iletisimler.Remove(İletisim);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool İletisimExists(int id)
        {
          return (_context.Iletisimler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
