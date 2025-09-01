using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoWorld3.Models;
using MotoWorld3.Data;

namespace MotoWorld3.Areas.Identity.Pages.Account.Manage
{
    public class ChatRoomListingsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ChatRoomListingsModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public List<Message> Rooms { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Rooms = await _context.Messages.Where(x => x.RoomID.Contains(user.Id)).Include(x => x.Advertising.IdentityUser).Include(x => x.SenderUser).ToListAsync();
        }
    }
}
