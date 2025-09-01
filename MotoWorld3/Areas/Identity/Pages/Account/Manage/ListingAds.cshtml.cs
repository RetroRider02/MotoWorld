using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoWorld3.Data;
using MotoWorld3.Models;

namespace MotoWorld3.Areas.Identity.Pages.Account.Manage
{
    public class ListingAdsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public List<MotorcycleAdvertising>? Advertisings { get; set; }

        public List<Picture>? Pictures { get; set; }

        public ListingAdsModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _dbContext = context;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var advertisings = await _dbContext.MotorcycleAdvertisings
                .Include(x => x.Advertising)
                .Include(x => x.Motorcycle)
                .Include(x => x.Motorcycle.MotorcycleType)
                .Where(x => x.Advertising.IdentityUserID == user.Id)
                .ToListAsync();
            
            var advertisingIDs = advertisings.Select(x => x.AdvertisingID).ToList();
            var images = await _dbContext.Pictures.Where(x => advertisingIDs.Contains(x.AdvertisingID)).GroupBy(x => x.AdvertisingID).Select(x => x.First()).ToListAsync();
            
            Advertisings = advertisings;
            Pictures = images;
        }
    }
}
