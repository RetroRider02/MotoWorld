using MotoWorld3.Models;
using MotoWorld3.Utilities;

namespace MotoWorld3.ViewModels.Advertisings
{
    public class ResultViewModel
    {
        public PaginatedList<MotorcycleAdvertising> PaginatedList { get; set; }
        public List<Picture> Pictures { get; set; }

        public ResultViewModel(PaginatedList<MotorcycleAdvertising> paginatedList, List<Picture> pictures)
        {
            PaginatedList = paginatedList;
            Pictures = pictures;
        }
    }
}
