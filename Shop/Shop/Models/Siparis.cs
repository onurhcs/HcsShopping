using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
	public class Siparis
	{
		[ScaffoldColumn(false)]
		public int SiparisId { get; set; }

		[ScaffoldColumn(false)]
		public string? UserId { get; set; } // TODO: ASP.NET Uyelik sistemindeki karsilik nesne konusulacak
		[ScaffoldColumn(false)]

		public DateTime? SiparisTarihi { get; set; }

		[Required(ErrorMessage = "Ad Soyad boş geçilemez")]
		public string AdSoyad { get; set; }

		[Required(ErrorMessage = "Teslimat Adresi boş geçilemez")]
		public string TeslimatAdresi { get; set; }

		[Required(ErrorMessage = "Sehir boş geçilemez")]
		public string Sehir { get; set; }

		[RegularExpression(@"^(05(\d{9}))$",
		ErrorMessage = "Lütfen Telefon Numaranızı doğru formatta giriniz.")]
		[Required(ErrorMessage = "Telefon boş geçilemez")]
		public string TelefonNumarasi { get; set; }

		
		[RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
		ErrorMessage = "Lütfen Email adresinizi doğru formatta giriniz.")]
		[Required(ErrorMessage = "Email boş geçilemez")]
		public string EPostaAdresi { get; set; }

		[ScaffoldColumn(false)]
		public decimal Tutar { get; set; }

		public string KuponKodu { get; set; }
		[Required(ErrorMessage = "Kart Numarası boş geçilemez")]

		public string KartNumarası { get; set; }
		[Required(ErrorMessage = "Kerdi Kartı Adı boş geçilemez")]

		public string KartUzerindekiAd { get; set; }
		[Required(ErrorMessage = "Son Kullanma Tarihi boş geçilemez")]

		public string SonKullanmaTarihi { get; set; }
		[Required(ErrorMessage = "Güvenlik Kodu boş geçilemez")]

		public int GüvenlikKodu { get; set; }
		public virtual IEnumerable<SiparisDetay>? SiparisDetaylari { get; set; }
	}
}
