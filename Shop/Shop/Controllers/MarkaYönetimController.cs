using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MarkaYönetimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarkaYönetimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MarkaYönetim
        public async Task<IActionResult> Index()
        {
              return _context.Markalar != null ? 
                          View(await _context.Markalar.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Markalar'  is null.");
        }

        // GET: MarkaYönetim/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Markalar == null)
            {
                return NotFound();
            }

            var marka = await _context.Markalar
                .FirstOrDefaultAsync(m => m.MarkaId == id);
            if (marka == null)
            {
                return NotFound();
            }

            return View(marka);
        }

        // GET: MarkaYönetim/Index
        public IActionResult Create()
        {
            return View();
        }

        // POST: MarkaYönetim/Index
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MarkaId,Ad")] Marka marka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marka);
        }

        // GET: MarkaYönetim/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Markalar == null)
            {
                return NotFound();
            }

            var marka = await _context.Markalar.FindAsync(id);
            if (marka == null)
            {
                return NotFound();
            }
            return View(marka);
        }

        // POST: MarkaYönetim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MarkaId,Ad")] Marka marka)
        {
            if (id != marka.MarkaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarkaExists(marka.MarkaId))
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
            return View(marka);
        }

        // GET: MarkaYönetim/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Markalar == null)
            {
                return NotFound();
            }

            var marka = await _context.Markalar
                .FirstOrDefaultAsync(m => m.MarkaId == id);
            if (marka == null)
            {
                return NotFound();
            }

            return View(marka);
        }

        // POST: MarkaYönetim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Markalar == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Markalar'  is null.");
            }
            var marka = await _context.Markalar.FindAsync(id);
            if (marka != null)
            {
                _context.Markalar.Remove(marka);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarkaExists(int id)
        {
          return (_context.Markalar?.Any(e => e.MarkaId == id)).GetValueOrDefault();
        }
    }
}
