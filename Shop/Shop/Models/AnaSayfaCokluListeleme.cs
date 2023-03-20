using System.Reflection.Metadata;



namespace Shop.Models
{
    public class AnaSayfaCokluListeleme
    {
        public IEnumerable<Kategori> Kategoriler { get; set; }
        public IEnumerable<Urun> Urunler { get; set; }
		public IEnumerable<Marka> Markalar { get; set; }
        public IEnumerable<İletisim> İletişimler { get; set; }
    }
}
