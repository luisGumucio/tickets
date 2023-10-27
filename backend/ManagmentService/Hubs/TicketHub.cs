using Microsoft.AspNetCore.SignalR;
using Ticketservice.Models;


namespace ManagmentService.Hubs
{
    public class TicketHub : Hub<ITicketClient>
    {
        public TicketHub()
		{
           
		}

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task AddNewTicket(Ticket ticketSignal)
        {
            await Console.Out.WriteLineAsync($"Notification sent: todo id {ticketSignal.ToString}");
            await Clients.All.AddNewTicket(ticketSignal);
        }
    }
}

