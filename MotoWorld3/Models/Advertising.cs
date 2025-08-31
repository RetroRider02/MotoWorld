using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MotoWorld3.Models
{
    public class Advertising
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string IdentityUserID { get; set; }

        [Required]
        public int PlaceID { get; set; }

        [Required, Range(0, 9990000)]
        [DisplayName("Ár")]
        public int Price { get; set; }

        [Required, MaxLength(1024)]
        [DisplayName("Leírás")]
        public string Description { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime LastModification { get; set; }

        [Required]
        public bool Frozen { get; set; }

        [ForeignKey("IdentityUserID")]
        public IdentityUser IdentityUser { get; set; }

        [ForeignKey("PlaceID")]
        public Place Place { get; set; }

        public ICollection<MotorcycleAdvertising> MotorcycleAdvertisings { get; set; }

        public ICollection<Message> Messages { get; set; }

        public Advertising(){}
    }
}
