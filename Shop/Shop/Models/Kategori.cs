namespace Shop.Models
{
    public class Kategori
    {
        public int KategoriId { get; set; }
        public string Ad { get; set; }
        public string? foto { get; set; }
        public string? Aciklama { get; set; }
        public virtual IEnumerable<Urun>? Urunler { get; set; }

    }
}
