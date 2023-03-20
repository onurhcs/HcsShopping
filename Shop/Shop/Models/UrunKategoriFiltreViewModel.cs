using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace Shop.Models
{
    public class UrunKategoriFiltreViewModel
    {
        public List<Urun>? Urunler { get; set; }
        public SelectList? Kategoriler { get; set; }
        public string? KategoriAdi { get; set; }
        public string? AramaMetni { get; set; }
    }
}
