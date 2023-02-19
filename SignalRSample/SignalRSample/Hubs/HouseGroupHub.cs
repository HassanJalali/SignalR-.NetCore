using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public class HouseGroupHub : Hub
    {
        // track user to figure out which group he/she joined or left
        public static List<string> GroupsJoined { get; set; } = new List<string>();

        public async Task JoinHouse(string houseName)
        {
            // check if connection not on that house
            // note that every connection have a uniq ID
            if (!GroupsJoined.Contains(Context.ConnectionId + ":" + houseName))
            {
                GroupsJoined.Add(Context.ConnectionId + ":" + houseName);

                string houseList = "";
                foreach (var str in GroupsJoined)
                {
                    if (str.Contains(Context.ConnectionId))
                    {
                        houseList += str.Split(':')[1] + " , ";
                    }
                }

                await Clients.Caller.SendAsync("subscriptionStatus", houseList, houseName.ToLower(), true);
                await Clients.Others.SendAsync("newMemberAddedToHouse", houseName);
                await Groups.AddToGroupAsync(Context.ConnectionId, houseName);
            }

        }

        public async Task LeaveHouse(string houseName)
        {
            if (GroupsJoined.Contains(Context.ConnectionId + ":" + houseName))
            {
                GroupsJoined.Remove(Context.ConnectionId + ":" + houseName);

                string houseList = "";
                foreach (var str in GroupsJoined)
                {
                    if (str.Contains(Context.ConnectionId))
                    {
                        houseList += str.Split(':')[1] + "";
                    }
                }
                await Clients.Caller.SendAsync("subscriptionStatus", houseList, houseName.ToLower(), false);
                //other than the caller
                await Clients.Others.SendAsync("newMemberRemovedToHouse", houseName);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
            }
        }

        public async Task TriggerHouseNotify(string houseName)
        {
            await Clients.Group(houseName).SendAsync("TriggerHouseNotification", houseName);
        }
    }
}
