using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;

namespace MotoWorld3.Models
{
    public class Motorcycle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MotorcycleTypeID { get; set; }

        [Required, NotNull, AllowedValues("Adventure", "Chopper", "Cruiser", "Egyedi", "Elektromos", "Enduro", "Modern Klasszikus", "Naked", "Robogó", "Sportmotor", "Sport-Túra", "Túra", "Vintage")]
        [DisplayName("Kategória")]
        public string Category { get; set; }

        [Required, Range(1910,2025)]
        [DisplayName("Gyártási év")]
        public int YearOfManufacture { get; set; }

        [Required, MaxLength(15)]
        [DisplayName("Szín")]
        public string Color { get; set; }

        [Required, AllowedValues("Újszerű", "Normál", "Sérült", "Enyhén sérült")]
        [DisplayName("Állapot")]
        public string Condition { get; set; }

        [Required, Range(1, 299000)]
        [DisplayName("Futásteljesítmény")]
        public int Mileage { get; set; }

        [Required, Range(45, 299)]
        [DisplayName("Saját tömeg")]
        public int Weight { get; set; }

        [Required, AllowedValues("Benzin")]
        [DisplayName("Üzemanyag")]
        public string Fuel { get; set; }

        [Required, Range(2, 199)]
        [DisplayName("Teljesítmény")]
        public int Power { get; set; }

        [Required, AllowedValues(2, 4)]
        [DisplayName("Munkaütem")]
        public int WorkSchedule { get; set; }

        [Required, AllowedValues(1, 2, 4, 6)]
        [DisplayName("Hengerek száma")]
        public int NumberOfCylinders { get; set; }

        [Required, Range(38, 1999)]
        [DisplayName("Hengerűrtartalom")]
        public int CylinderCapacity { get; set; }

        [Required, AllowedValues(2, 4)]
        [DisplayName("Szelepek száma hengerenként")]
        public int ValvesPerCylinders { get; set; }
        
        [Required, AllowedValues("Soros", "Boxer", "V-2", "V-4")]
        [DisplayName("Henger elrendezés")]
        public string CylinderArrangement { get; set; }
        
        [Required, AllowedValues("Injektor", "Karburátor")]
        [DisplayName("Keverékképzés")]
        public string Mixture { get; set; }

        [Required, AllowedValues("Víz", "Lég", "Lég-Olaj")]
        [DisplayName("Hűtés")]
        public string Cooling { get; set; }

        [Required, AllowedValues("Szíj", "Lánc", "Kardán")]
        [DisplayName("Hajtás")]
        public string DriveType { get; set; }

        [Required, AllowedValues("Manuális", "Automata")]
        [DisplayName("Váltó típusa")]
        public string Transmission { get; set; }

        [ForeignKey("MotorcycleTypeID")]
        public MotorcycleType MotorcycleType { get; set; }

        public ICollection<MotorcycleAdvertising> MotorcycleAdvertisings { get; set; }
    }
}
