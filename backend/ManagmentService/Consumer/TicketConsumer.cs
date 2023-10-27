using Ticketservice.Models;
using MassTransit;
using ManagmentService.Hubs;
using ManagmentService.Models;
using Microsoft.AspNetCore.SignalR;

namespace ManagmentService.Consumer
{
    public class TicketConsumer : IConsumer<Ticket>
    {
        private IHubContext<TicketHub, ITicketClient> _ticketHub;
        public TicketConsumer(IHubContext<TicketHub, ITicketClient> ticketHub)
		{
			_ticketHub = ticketHub;
		}

		public async Task Consume(ConsumeContext<Ticket> context)
		{
			await Console.Out.WriteLineAsync($"Notification sent: udpate ticket {context.Message.TicketId}");
			//var ticket = new TicketSignal("ticket", context.Message);
			await _ticketHub.Clients.All.AddNewTicket(context.Message);
		}
	}
}