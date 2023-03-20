using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Marka> Markalar { get; set; }
        public DbSet<İletisim> Iletisimler { get; set; }


		public DbSet<SepetElemani> SepetElemanlari { get; set; }
		public DbSet<Siparis> Siparisler { get; set; }
		public DbSet<SiparisDetay> SiparisDetaylari { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}