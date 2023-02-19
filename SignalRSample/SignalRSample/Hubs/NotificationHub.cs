using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public class NotificationHub : Hub
    {
        public List<string> messages = new List<string>();
        public static int notificationCounter = 0;

        public async Task SendMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                notificationCounter++;
                messages.Add(message);
            }
            await LoadMessages();
        }

        public async Task LoadMessages()
        {
            await Clients.All.SendAsync("LoadNotification", messages, notificationCounter);

        }
    }
}
