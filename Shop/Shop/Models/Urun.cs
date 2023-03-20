using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Urun
    {
        public int Id { get; set; }
        
        public string Ad { get; set; }
       
        public decimal Fiyat { get; set; }
     
        public string? Aciklama { get; set; }

        [Display(Name = "İşlemci Tipi")]
        public string? İşlemciModel { get; set; }

        [Display(Name = "Ekran Kartı")]
        public string? GPUModel { get; set; }

        [Display(Name = "Ram (Sistem Belleği)")]
        public string? RamKapasitesi { get; set; }

        [Display(Name = "Ekran Boyutu")]
        public string? EkranBoyutu { get; set; }

        [Display(Name = "İşletim Sistemi")]
        public string? İşletimSistemi { get; set; }

        [Display(Name = "Görsel Dosya Adı")]
        public string? Foto { get; set; }

        //[Display(Name = "Görsel1")]
        //public string? Foto1 { get; set; }

        //[Display(Name = "Görsel2")]
        //public string? Foto2 { get; set; }

        //[Display(Name = "Görsel3")]
        //public string? Foto3 { get; set; }

        public int KategoriId { get; set; }
        public virtual Kategori? Kategori { get; set; }
        public int MarkaId { get; set; }
        public virtual Marka? Marka { get; set; }
    }
}
