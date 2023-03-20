namespace Shop.Models
{
    public class Marka
    {
        public int MarkaId { get; set; }
        public string Ad { get; set; }

        public virtual IEnumerable<Urun>? Uruns { get; set; }
    }
}
