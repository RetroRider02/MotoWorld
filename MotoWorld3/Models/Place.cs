using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MotoWorld3.Models
{
    public class Place
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Irányítószám")]
        public int ZipCode { get; set; }

        [Required, MaxLength(30)]
        [DisplayName("Helységnév")]
        public string CityName { get; set; }

        [Required, MaxLength(30)]
        [DisplayName("Utca")]
        public string Street { get; set; }

        [Required, MaxLength(10)]
        [DisplayName("Házszám")]
        public string HouseNumber { get; set; }

        public Place() { }
    }
}
