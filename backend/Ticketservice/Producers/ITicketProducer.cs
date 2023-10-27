using Ticketservice.Models;

namespace Ticketservice.Producers;

public interface ITicketProducer
{
    void addNewTicket(Ticket ticket);
}