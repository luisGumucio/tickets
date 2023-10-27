using System;
using Ticketservice.Models;

namespace ManagmentService.Producer
{
	public interface ITicketProducer
	{
		void updateTicket(Ticket ticket);
	}
}

