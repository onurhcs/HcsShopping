using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class İletisim
    {
        public int Id { get; set; }
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; }

        [Display(Name = "Telefon No")]
        public string TelefonNo { get; set; }

        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string EMail { get; set; }


        public string Konu { get; set; }
        [Display(Name = "Mesaj")]
        public string MesajMetni { get; set; }

        public DateTime Tarih { get; set; }
        public bool Okundu { get; set; }
        public DateTime OkunmaTarihi { get; set; }
    }
}
