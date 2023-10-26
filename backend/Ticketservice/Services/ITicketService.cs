using Ticketservice.Models;

namespace Ticketservice.Services;

public interface ITicketService
{
    Task<Ticket> CreateTicket(Ticket ticket);
    IEnumerable<Ticket> FindByEmail(string email);
    IEnumerable<Ticket> GetAllTickets();
}