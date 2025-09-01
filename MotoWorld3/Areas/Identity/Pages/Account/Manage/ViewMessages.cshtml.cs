using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoWorld3.Data;
using MotoWorld3.Models;

namespace MotoWorld3.Areas.Identity.Pages.Account.Manage
{
    public class ViewMessagesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ViewMessagesModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public List<Message>? Messages { get; set; }

        public int AdvertisingID { get; set; }

        public MotorcycleAdvertising? MotorcycleAdvertising { get; set; }

        public string? SenderEmail { get; set; }

        public string? SenderID { get; set; }

        public string? RoomID { get; set; }

        public async Task OnGetAsync(int id, string? room = null)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            AdvertisingID = id;
            SenderID = currentUser?.Id;
            SenderEmail = currentUser?.Email;
            MotorcycleAdvertising = await _context.MotorcycleAdvertisings.Where(x => x.AdvertisingID == id)
                    .Include(x => x.Advertising.IdentityUser)
                    .Include(x => x.Motorcycle.MotorcycleType).FirstAsync();

            if (room == null)
            {
                var otherUser = await _context.Advertisings.Where(x => x.Id == id).Select(x => x.IdentityUserID).FirstAsync();
                RoomID = string.Join("-", new[] { currentUser?.Id, otherUser, Convert.ToString(id) }.OrderBy(x => x));
            }
            else
            {
                RoomID = room;
            }

            Messages = await _context.Messages.Where(x => x.RoomID == RoomID).Include(x => x.SenderUser).ToListAsync();
        }
    }
}
