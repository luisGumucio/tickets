using System;
using ManagmentService.Models;
using Ticketservice.Models;

namespace ManagmentService.Hubs
{
	public interface ITicketClient
	{
		Task AddNewTicket(Ticket ticketSignal);
	}
}

