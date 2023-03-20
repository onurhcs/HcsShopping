using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System.Drawing;

namespace Shop.Helpers
{
    public class Sepet
    {
        ApplicationDbContext context;
        public string SepetAdi { get; set; }

        const string SepetSessionKey = "SepetAdi";

        private Sepet(ApplicationDbContext ctx)
        {
            this.context = ctx;
        }

        public static Sepet SepetiGetir(ApplicationDbContext db, HttpContext http)
        {
            // hafızada sepet var ise varolani getir
            // yok ise,
            // kisi login durumda ise bos sepet olustur adına username ver
            // login degil ise bos sepet olustur adına guid ver.

            Sepet spt = new Sepet(db);
            spt.SepetAdi = spt.SepetAdiGetir(http); // varolan,login olan Username,yeni guid;
            return spt;
        }

        private string SepetAdiGetir(HttpContext http)
        {
            // hafızada sepet var ise varolani getir
            string sepetAdi = http.Session.GetString(SepetSessionKey);

            // yok ise,
            if (sepetAdi == null)
            {

                string kullanicininAdi = http.User.Identity.Name;
                // gelen bilgi null degil ise login biri vardir.

                if (String.IsNullOrWhiteSpace(kullanicininAdi))
                {
                    // login degil ise bos sepet olustur adına guid ver.
                    Guid tempCartId = Guid.NewGuid();

                    http.Session.SetString(SepetSessionKey, tempCartId.ToString());
                }
                else
                {
                    // kisi login durumda ise bos sepet olustur adına username ver
                    http.Session.SetString(SepetSessionKey, kullanicininAdi);
                }
            }

            // hafizadaki sepeti return et
            return http.Session.GetString(SepetSessionKey);
        }

        // parametre olarak kitap alan
        // iceride sepet elemani nesnesi örnekleyen (hafizaya cikarak) (bu kitaptan bu sepette yok ise)
        // bu bilgiyi veri tabanına kaydeden
        // metodu kodlayınız
        //
        // bu sepette o urunden zaten var ise adedi artmali
        public void SepeteAt(Urun urn)
        {
            // Get the matching cart and album instances
            SepetElemani urun = context.SepetElemanlari.SingleOrDefault(c => c.SepetAd == this.SepetAdi && c.UrunId == urn.Id);
            // bu sepette bu urun yok ise null gelir, var ise satiri tasiyan nesne gelir.

            if (urun == null)
            {
                // yok ise sepet elemani nesnesini 1 adet olarak hazirla
                urun = new SepetElemani()
                {
                    UrunId = urn.Id,
                    SepetAd = this.SepetAdi,
                    Adet = 1,
                    EklenmeTarihi = DateTime.Now,
                    SatisFiyati = urn.Fiyat
                };

                context.SepetElemanlari.Add(urun);
            }
            else
            {
                // Ürün sepette varsa adedi 1 artir
                urun.Adet++;
            }

            // veri tabanina kaydet
            context.SaveChanges();
        }

        // sepeti tamamen bosaltan kod
        public void SepetiBosalt()
        {
            // bu sepetteki tum urunleri getir
            // yani; bu isimdeki sepete ait veri tabanında kayitli urunleri getir
            IQueryable<SepetElemani> sepettekiElemenlar = context.SepetElemanlari.Where(e => e.SepetAd == this.SepetAdi);

            // döngüye git ve adetlerine bakmaksızın her urun bilgisini tablodan tek tek sil
            foreach (SepetElemani urun in sepettekiElemenlar)
            {
                context.SepetElemanlari.Remove(urun);
            }

            // degisiklikleri veri tabanına yansıt (delete from tabloadı)
            context.SaveChanges();
        }

        public int SepettenUrunCikar(Urun k)
        {
            return SepettenUrunCikar(k.Id);
        }


        /// <summary>
        /// sepetten urun cikartır, kalan adedi return eder
        /// </summary>
        /// <param name="id">KitapId degeri</param>
        /// <returns></returns>
        public int SepettenUrunCikar(int id)
        {
            // Get the cart
            SepetElemani cikarilmakIstenenUrun = context.SepetElemanlari.SingleOrDefault(se => se.SepetAd == this.SepetAdi && se.UrunId == id);

            int kalanAdet = 0;

            if (cikarilmakIstenenUrun != null)
            {
                if (cikarilmakIstenenUrun.Adet > 1)
                {
                    cikarilmakIstenenUrun.Adet--;
                    kalanAdet = cikarilmakIstenenUrun.Adet;
                }
                else
                {
                    context.SepetElemanlari.Remove(cikarilmakIstenenUrun);
                }
                // Save changes
                context.SaveChanges();
            }

            return kalanAdet;
        }


        // Sepet Elemanlarını getir (ihtiyac duyulan tum bilgiler geliyor mu???)
        public List<SepetElemani> SepettekiElemanlariGetir()
        {
            return context.SepetElemanlari.Where(e => e.SepetAd == this.SepetAdi).Include(e => e.Urunler).ToList();
        }

        public int SepettekiElemanAdediniGetir()
        {
            // 2 kalem 1 silgi var ise cevap olarak 3 gelmeli
            // bize gereken bilgi tablodaki satirlarin adetlerinin toplami

            // NOT: satır bulunamazsa error vermemeli, 0 dönmeli
            var urunler = context.SepetElemanlari.Where(e => e.SepetAd == this.SepetAdi);
            if (urunler == null)
            {
                return 0;
            }

            return urunler.Sum(u => u.Adet);
        }

        public decimal SepetTutariniHesapla()
        {
            var urunler = SepettekiElemanlariGetir();
            if (urunler == null)
            {
                return 0;
            }

            return urunler.Sum(u => u.Adet * u.SatisFiyati);
        }

        // TODO: login oldugumuz anda bu metot cagrilmali. register ya da aktivasyonda auto login var ise, o metodun icinde de bu metot calismali.
        public void SepetiSahiplen(string userName)
        {
            var elemanlar = context.SepetElemanlari.Where(e => e.SepetAd == this.SepetAdi);
            foreach (SepetElemani eleman in elemanlar)
            {
                eleman.SepetAd = userName;
            }

            context.SaveChanges();
        }

        public int SiparisinDetaylariniEkle(Siparis siparis)
        {
            var urunler = SepettekiElemanlariGetir();

            foreach (SepetElemani urun in urunler)
            {
                // urunden siparis detayi elde et
                // siparis detayini ile siparisi birbirine bagla
                SiparisDetay sd = new SiparisDetay() { UrunId = urun.UrunId, SatisFiyati = urun.SatisFiyati, SiparisId = siparis.SiparisId, SiparisAdedi = urun.Adet };

                // detayi veri tabanına ekle
                context.SiparisDetaylari.Add(sd);


            }

            context.SaveChanges();

            SepetiBosalt();

            return siparis.SiparisId;
        }

        // veri tabanı erisimi icin ApplicationDbContext
        // Ram erisimi icin HttpContext (Controller üzerinde yer aliyor, controller kendine ait bilgiyi sepete vermeli)
        // login olan kimdir ulasabilmek
    }
}
