using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
	public class UrunController : Controller
	{
		private readonly ApplicationDbContext _db;

		public UrunController(ApplicationDbContext context)
		{
			_db = context;
		}
		public IActionResult Index(int kategoriId)
		{
			List<Urun> Urunler =  _db.Urunler.Include(k => k.Kategori).Where(k => k.Kategori.KategoriId == kategoriId).ToList();
			return View(Urunler);
		}
        public async Task<IActionResult> UrunDetay(int? id)
		{
			if (id == null || _db.Urunler == null)
			{
				return NotFound();
			}
			var urundt = await _db.Urunler.Where(x => x.Id == id).ToListAsync();

			if (urundt == null)
			{
				return NotFound();
			}
			ViewData["BenzerUrunler"] = _db.Urunler.OrderBy(x => Guid.NewGuid()).Take(4).ToList(); // Take: Rastgele 4 Adet Urun Getirme
			return View(urundt);
		}
	}
}
