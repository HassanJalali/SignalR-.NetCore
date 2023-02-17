using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;

namespace SignalRSample.Hubs
{
    //count the number of views on a web page
    public class UserHub : Hub
    {
        public static int TotalViews { get; set; } = 0;
        public static int TotalUser { get; set; } = 0;

        //track number of connection using 

        public override Task OnConnectedAsync()
        {
            TotalUser++;
            Clients.All.SendAsync("UpdateTotalUsers", TotalUser).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUser--;
            Clients.All.SendAsync("UpdateTotalUsers", TotalUser).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }


        // called whenever page loaded or reload
        public async Task<string> NewWindowLoaded()
        {
            TotalViews++;
            await Clients.All.SendAsync("UpdateTotalViews", TotalViews);
            return $"Total Views - {TotalViews}";
        }

    }
}
