using Shop.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System.Drawing;

namespace Shop.Controllers
{
    public class SepetController : Controller
    {
        ApplicationDbContext db;

        public SepetController(ApplicationDbContext context)
        {
            db = context;
        }


        // GET: /ShoppingCart/
        public IActionResult Index()
        {
            Sepet sepetim = Sepet.SepetiGetir(db, HttpContext);

            SepetViewModel vm = new SepetViewModel();
            vm.SepetElemanlari = sepetim.SepettekiElemanlariGetir();
            vm.SepetTutari = sepetim.SepetTutariniHesapla();
            vm.SepettekiElemanAdedi = sepetim.SepettekiElemanAdediniGetir();

            return View(vm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">kitap id</param>
        /// <returns></returns>
        public IActionResult SepeteUrunEkle(int id)
        {
            // veri tabanından kitabı getir
            Urun? eklenecekUrun = db.Urunler.SingleOrDefault(k => k.Id == id);

            // sepete eriş
            Sepet sepetim = Sepet.SepetiGetir(db, HttpContext);

            // kitabi sepete at
            sepetim.SepeteAt(eklenecekUrun);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
        public IActionResult SepetiBosalt()
        {

            Sepet sepetim = Sepet.SepetiGetir(db, HttpContext);

            // Urunu sepete at
            sepetim.SepetiBosalt();

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        // AJAX: /ShoppingCart/RemoveFromCart/5
        // json result dönüyoruz.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">silinecek kitapId degeri</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SepettenUrunSil(int id)
        {
            // Remove the item from the cart

            // sepeti getir
            Sepet sepetim = Sepet.SepetiGetir(db, HttpContext);

            // Mesajda goruntulemek icin kitap adi getir
            string kitapAdi = db.SepetElemanlari.Include(se => se.Urunler).SingleOrDefault(se => se.UrunId == id && se.SepetAd == sepetim.SepetAdi).Urunler.Ad;

            // sepetten urun cikart ve kalan adedi elde et
            int ilgiliUrundenKalanAdet = sepetim.SepettenUrunCikar(id);

            // Display the confirmation message
            SepettenSilViewModel results = new SepettenSilViewModel()
            {

                Mesaj = kitapAdi + " adlı Urun sepetten çıkarıldı.",
                SepetTutari = sepetim.SepetTutariniHesapla(),
                SepettekiElemanAdedi = sepetim.SepettekiElemanAdediniGetir(),
                IlgiliUrunAdedi = ilgiliUrundenKalanAdet,
                SilinenUrunId = id
            };

            return Json(results);
        }
    }
}
