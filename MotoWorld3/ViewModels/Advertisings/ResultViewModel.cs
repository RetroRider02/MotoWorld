using MotoWorld3.Models;
using MotoWorld3.Utilities;
using System.ComponentModel.DataAnnotations;

namespace MotoWorld3.ViewModels.Advertisings
{
    public class ResultViewModel
    {
        [MaxLength(30)]
        public string? Manufacturer { get; set; }

        [MaxLength(50)]
        public string? Model { get; set; }

        [Range(0, 350000)]
        public int? MinMileage { get; set; }

        [Range(0, 350000)]
        public int? MaxMileage { get; set; }

        [Range(1900, 2025)]
        public int? MinYearOfManufacture { get; set; }

        [Range(1900, 2025)]
        public int? MaxYearOfManufacture { get; set; }

        [Range(0, 9990000)]
        public int? MinPrice { get; set; }

        [Range(0, 9990000)]
        public int? MaxPrice { get; set; }

        [AllowedValues("Benzin")]
        public string? Fuel { get; set; }

        [Range(38, 1999)]
        public int? MinCylinderCapacity { get; set; }

        [Range(38, 1999)]
        public int? MaxCylinderCapacity { get; set; }

        public PaginatedList<MotorcycleAdvertising> PaginatedList { get; set; }
        
        public List<Picture> Pictures { get; set; }

        public ResultViewModel(PaginatedList<MotorcycleAdvertising> paginatedList, List<Picture> pictures)
        {
            PaginatedList = paginatedList;
            Pictures = pictures;
        }
    }
}
