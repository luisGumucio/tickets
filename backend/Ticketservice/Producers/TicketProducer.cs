using Ticketservice.Models;
using MassTransit;
namespace Ticketservice.Producers;

public class TicketProducer : ITicketProducer
{
    private readonly IBus _bus;
    public TicketProducer(IBus bus) {
        _bus = bus;
    }

    public async void addNewTicket(Ticket ticket) {
        Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
        var endpoint = await _bus.GetSendEndpoint(uri);
        await endpoint.Send(ticket);
    }
}