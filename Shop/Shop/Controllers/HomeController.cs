using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System.Diagnostics;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
		private readonly ApplicationDbContext _db;
		AnaSayfaCokluListeleme _As = new AnaSayfaCokluListeleme();

		public HomeController(ApplicationDbContext context)
		{
			_db = context;
		}


		public IActionResult Index()
		{
			_As.Kategoriler = _db.Kategoriler.ToList();
			_As.Urunler = _db.Urunler.ToList();
			return View(_As);
		}
		public IActionResult Hakkımızda()
		{
			_As.Kategoriler = _db.Kategoriler.ToList();
			_As.Urunler = _db.Urunler.ToList();
			return View(_As);
		}
	
		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}