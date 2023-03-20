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
    public class SiparisYonetimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SiparisYonetimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SiparisYonetim
        public async Task<IActionResult> Index()
        {
              return _context.Siparisler != null ? 
                          View(await _context.Siparisler.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Siparisler'  is null.");
        }

        // GET: SiparisYonetim/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Siparisler == null)
            {
                return NotFound();
            }

            var siparis = await _context.Siparisler
                .FirstOrDefaultAsync(m => m.SiparisId == id);
            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }

        // GET: SiparisYonetim/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SiparisYonetim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdSoyad,TeslimatAdresi,Sehir,TelefonNumarasi,EPostaAdresi,KuponKodu")] Siparis siparis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siparis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siparis);
        }

        // GET: SiparisYonetim/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Siparisler == null)
            {
                return NotFound();
            }

            var siparis = await _context.Siparisler.FindAsync(id);
            if (siparis == null)
            {
                return NotFound();
            }
            return View(siparis);
        }

        // POST: SiparisYonetim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdSoyad,TeslimatAdresi,Sehir,TelefonNumarasi,EPostaAdresi,KuponKodu")] Siparis siparis)
        {
            if (id != siparis.SiparisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siparis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiparisExists(siparis.SiparisId))
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
            return View(siparis);
        }

        // GET: SiparisYonetim/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Siparisler == null)
            {
                return NotFound();
            }

            var siparis = await _context.Siparisler
                .FirstOrDefaultAsync(m => m.SiparisId == id);
            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }

        // POST: SiparisYonetim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Siparisler == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Siparisler'  is null.");
            }
            var siparis = await _context.Siparisler.FindAsync(id);
            if (siparis != null)
            {
                _context.Siparisler.Remove(siparis);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiparisExists(int id)
        {
          return (_context.Siparisler?.Any(e => e.SiparisId == id)).GetValueOrDefault();
        }
    }
}
