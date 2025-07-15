using MotoWorld3.Models;

namespace MotoWorld3.ViewModels.Advertisings
{
    public class DetailsViewModel
    {
        public MotorcycleAdvertising MotorcycleAdvertising { get; set; }
        public List<Picture> Pictures { get; set; }

        public DetailsViewModel(MotorcycleAdvertising motorcycleAdvertising, List<Picture> pictures)
        {
            MotorcycleAdvertising = motorcycleAdvertising;
            Pictures = pictures;
        }
    }
}
