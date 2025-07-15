using MotoWorld3.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MotoWorld3.ViewModels.Home
{
    public class IndexViewModel
    {
        [MaxLength(30)]
        public string? Manufacturer { get; set; }

        [Range(0, 350000)]
        public int? MinMileage { get; set; }

        [Range(0, 350000)]
        public int? MaxMileage { get; set; }

        [Range(1900,2025)]
        public int? MinYearOfManufacture { get; set; }

        [Range(1900, 2025)]
        public int? MaxYearOfManufacture { get; set; }

        public List<MotorcycleAdvertising> MotorcycleAdvertising { get; set; }

        public List<Picture> Pictures { get; set; }

        public IndexViewModel(List<MotorcycleAdvertising> motorcycleAdvertising, List<Picture> pictures)
        {
            MotorcycleAdvertising = motorcycleAdvertising;
            Pictures = pictures;
        }
    }
}
