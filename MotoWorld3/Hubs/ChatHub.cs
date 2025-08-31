using Microsoft.AspNetCore.SignalR;
using MotoWorld3.Data;
using MotoWorld3.Models;

namespace MotoWorld3.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        // Csatlakozás egy szobához
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        // Kilépés a szobából (opcionális)
        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }

        // Üzenet küldése a szobába
        public async Task SendMessage(int advertisingID, string roomID, string senderID, string senderEmail, string message)
        {
            _context.Messages.Add(new Message()
            {
                AdvertisingID = advertisingID,
                RoomID = roomID,
                SenderID = senderID,
                Content = message,
                Created = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            await Clients.Group(roomID).SendAsync("ReceiveMessage", senderID, senderEmail, message);
        }
    }
}
