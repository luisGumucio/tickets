using Ticketservice.Models;
using Ticketservice.Services;
using MassTransit;

namespace Ticketservice.Consumer;

public class TicketConsumer : IConsumer<Ticket>
{
	private ITicketService _ticketService;

	public TicketConsumer(ITicketService ticketservice)
	{
		_ticketService = ticketservice;
	}
	public async Task Consume(ConsumeContext<Ticket> context)
	{
			await Console.Out.WriteLineAsync($"Notification sent: todo id {context.Message}");
		_ticketService.UpdateTicket(context.Message);
     }
}