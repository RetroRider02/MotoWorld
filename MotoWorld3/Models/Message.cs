using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoWorld3.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AdvertisingID { get; set; }

        [Required]
        public string IdentityUserID { get; set; }

        [Required, MaxLength(1024)]
        [DisplayName("Tartalom")]
        public string Content { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [ForeignKey("AdvertisingID")]
        public Advertising Advertising { get; set; }

        [ForeignKey("IdentityUserID")]
        public IdentityUser IdentityUser { get; set; }
    }
}
