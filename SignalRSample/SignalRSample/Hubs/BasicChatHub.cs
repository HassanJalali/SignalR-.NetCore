using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SignalRSample.Data;
using System.Reflection;

namespace SignalRSample.Hubs
{
    public class BasicChatHub : Hub
    {

        private readonly ApplicationDbContext _context;
        public BasicChatHub(ApplicationDbContext context)
        {
            _context = context;
        }
        public static List<string> messageList = new List<string>();

        [Authorize]
        public async Task SendMessageToAll(string sender, string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                messageList.Add(message);
                await Clients.All.SendAsync("MessageRecieved", sender, message);
            }
        }

        [Authorize]
        public async Task SendMessageToPrivate(string sender, string Reciever, string message)
        {
            bool loginUser = _context.UserLogins.Any(u => u.UserId == Reciever);
            var userId = _context.Users.FirstOrDefault(u => u.Email.ToLower() == Reciever.ToLower()).Id;

            if (!string.IsNullOrWhiteSpace(message) && !string.IsNullOrWhiteSpace(userId))
            {
                messageList.Add(message);
                await Clients.User(userId).SendAsync("MessageRecieved", sender, message);
            }

        }
    }
}
