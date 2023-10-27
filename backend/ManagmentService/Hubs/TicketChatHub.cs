using ManagmentService.Producer;
using Microsoft.AspNetCore.SignalR;
using Ticketservice.Models;

namespace ManagmentService.Hubs
{
	public class TicketChatHub : Hub
	{
        private ITicketProducer _ticketProducer;
        public TicketChatHub(ITicketProducer ticketConsumer)
		{
            _ticketProducer = ticketConsumer;
        }

        public async Task JoinGroup(string email, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, email);
        }

        public async Task SendMessage(NewMessage message)
        {
            await Clients.Group(message.GroupName).SendAsync("NewMessage", message);
        }

        public async Task UdpateTicket(Ticket ticket)
        {
            await Console.Out.WriteLineAsync(ticket.Email);
            _ticketProducer.updateTicket(ticket);
        }
    }
}

