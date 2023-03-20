using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using System.Data;

namespace Shop.Controllers
{
   
        [Authorize(Roles = "Admin")]
        public class AdminAnaSayfaController : Controller
        {
            private readonly ApplicationDbContext _db;
            AnaSayfaCokluListeleme _As = new AnaSayfaCokluListeleme();

            public AdminAnaSayfaController(ApplicationDbContext context)
            {
                _db = context;
            }
            public IActionResult Index()
            {
                _As.Kategoriler = _db.Kategoriler.ToList();
                _As.Urunler = _db.Urunler.ToList();
                _As.Markalar = _db.Markalar.ToList();
            //_As.Bloglar = _db.Bloglar.ToList();
                 _As.İletişimler = _db.Iletisimler.ToList();
            return View(_As);


            }

        }
    
}
