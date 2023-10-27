using System;
using MassTransit;
using Ticketservice.Models;

namespace ManagmentService.Producer
{
	public class TicketProducer : ITicketProducer
	{
        private readonly IBus _bus;
        public TicketProducer(IBus bus)
        {
            _bus = bus;
        }

        public async void updateTicket(Ticket ticket)
        {
            Uri uri = new Uri("rabbitmq://localhost/ticketUpdateQueue");
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send(ticket);
        }
    }
}

