
namespace MotoWorld3.Models
{
    public class MotorcycleAdvertising
    {
        public int MotorcycleID { get; set; }
        public int AdvertisingID { get; set; }

        public Motorcycle Motorcycle { get; set; }
        public Advertising Advertising { get; set; }

        public MotorcycleAdvertising() { }
    }
}
