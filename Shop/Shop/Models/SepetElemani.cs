using System.Drawing;

namespace Shop.Models
{
	public class SepetElemani
	{
		public int Id { get; set; }

		public int UrunId { get; set; }
		public virtual Urun? Urunler { get; set; }

		public decimal SatisFiyati { get; set; }
		public int Adet { get; set; }

		public string SepetAd { get; set; } // TODO: sepet nesnesi yapmadik,
											// username ya da emaili sepet adı olarak kullanmayı planliyoruz.
		public DateTime EklenmeTarihi { get; set; }
	}
}
