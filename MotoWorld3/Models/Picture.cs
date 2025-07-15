using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotoWorld3.Models
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AdvertisingID { get; set; }

        [Required, MaxLength(75)]
        public string FileName { get; set; }

        [Required, MaxLength(15)]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }
        
        [ForeignKey("AdvertisingID")]
        public Advertising Advertising { get; set; }

        public Picture() { }
    }
}
