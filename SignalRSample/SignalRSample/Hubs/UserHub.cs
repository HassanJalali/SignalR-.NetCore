using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    //count the number of views on a web page
    public class UserHub : Hub
    {
        public static int TotalViews { get; set; } = 0;

        // called whenever page loaded or reload
        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await Clients.All.SendAsync("UpdateTotalViews", TotalViews);
        }

    }
}
