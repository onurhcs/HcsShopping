using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UrunYönetimController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environmet;

        public UrunYönetimController(ApplicationDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            _environmet= environment;
        }

        // GET: UrunYönetim
        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Urunler.Include(u => u.Kategori).Include(u => u.Marka);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UrunYönetim/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler
                .Include(u => u.Kategori)
                .Include(u => u.Marka)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }
        
        // GET: UrunYönetim/Index
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "Ad");
            ViewData["MarkaId"] = new SelectList(_context.Markalar, "MarkaId", "Ad");
            return View();
        }

        // POST: UrunYönetim/Index
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Fiyat,Aciklama,İşlemciModel,GPUModel,RamKapasitesi,EkranBoyutu,İşletimSistemi,Foto,KategoriId,MarkaId")]
        Urun urun, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {

				var photoName = Guid.NewGuid().ToString()
					 + System.IO.Path.GetExtension(Foto.FileName);

				using (var strem = new FileStream(
                    Path.Combine(_environmet.WebRootPath, "img/", photoName),
                    FileMode.Create))
                {
                    await Foto.CopyToAsync(strem);
                    urun.Foto =photoName;
                }

				_context.Add(urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriId", urun.KategoriId);
            ViewData["MarkaId"] = new SelectList(_context.Markalar, "MarkaId", "MarkaId", urun.MarkaId);
            return View(urun);
        }

        // GET: UrunYönetim/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriId", urun.KategoriId);
            ViewData["MarkaId"] = new SelectList(_context.Markalar, "MarkaId", "MarkaId", urun.MarkaId);
            return View(urun);
        }

        // POST: UrunYönetim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Fiyat,Aciklama,İşlemciModel,GPUModel,RamKapasitesi,EkranBoyutu,İşletimSistemi,Foto,KategoriId,MarkaId")] Urun urun)
        {
            if (id != urun.Id)
            {
                


                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //if ( dosya != null)
                    //{
                    //    string dosyaIsmi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);

                    //    string dosyaAdi = Path.Combine(_environment.WebRootPath, "img/", urun.Foto);
                    //    using(FileStream stream = new FileStream(dosyaAdi, FileMode.Index))
                    //    {
                    //        await dosya.CopyToAsync(stream);
                    //    }

                    //    string silinecek = Path.Combine(_environment.WebRootPath, "img/", urun.Foto);
                    //    System.IO.File.Delete(silinecek);

                    //    urun.Foto = dosyaIsmi;
                    //}





                    _context.Update(urun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunExists(urun.Id))
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
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriId", urun.KategoriId);
            ViewData["MarkaId"] = new SelectList(_context.Markalar, "MarkaId", "MarkaId", urun.MarkaId);
            return View(urun);
        }

        // GET: UrunYönetim/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler
                .Include(u => u.Kategori)
                .Include(u => u.Marka)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // POST: UrunYönetim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Urunler == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Urunler'  is null.");
            }
            var urun = await _context.Urunler.FindAsync(id);
            if (urun != null)
            {
                _context.Urunler.Remove(urun);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunExists(int id)
        {
          return (_context.Urunler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
