using Shop.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{

	[Authorize]
	public class OdemeController : Controller
	{

		ApplicationDbContext db;

		public OdemeController(ApplicationDbContext context)
		{
			db = context;
		}
		public ActionResult AddressAndPayment()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddressAndPayment(Siparis siparis)

		{

			if (!ModelState.IsValid)
			{
				return View(siparis);
			}

			if (siparis.KuponKodu != "FREE")
			{
				return View(siparis);
			}

			siparis.UserId = User.Identity.Name;
			siparis.SiparisTarihi = DateTime.Now;

			//Save Order
			db.Siparisler.Add(siparis);
			db.SaveChanges();

			//Process the siparis
			var cart = Sepet.SepetiGetir(db, this.HttpContext);
			decimal tutar = cart.SepetTutariniHesapla();
			cart.SiparisinDetaylariniEkle(siparis);
			siparis.Tutar = tutar;
			db.SaveChanges();

			return RedirectToAction("Complete", new { id = siparis.SiparisId });

		}

		public ActionResult Complete(int id)
		{
			// Validate customer owns this siparis
			bool isValid = db.Siparisler.Any(o => o.SiparisId == id && o.UserId == User.Identity.Name);

			if (isValid)
			{
				return View(id);
			}
			else
			{
				return View("Error");
			}
		}
	}
}
