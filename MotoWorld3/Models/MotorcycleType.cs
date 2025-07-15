using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MotoWorld3.Models
{
    public class MotorcycleType
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        [DisplayName("Gyártó")]
        public string Manufacturer { get; set; }

        [Required, MaxLength(50)]
        [DisplayName("Modell")]
        public string Model { get; set; }
    }
}
