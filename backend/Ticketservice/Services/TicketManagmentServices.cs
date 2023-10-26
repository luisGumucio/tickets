using Ticketservice.Config;
using Ticketservice.Models;

namespace Ticketservice.Services;

public class TicketManagmentservice : ITicketService
{
    private readonly ConnectionDatabase _connectionDatabase;

    public TicketManagmentservice(ConnectionDatabase connectionDatabase)
    {
        _connectionDatabase = connectionDatabase;
    }

    public IEnumerable<Ticket> GetAllTickets()
    {
        return _connectionDatabase.Tickets;
    }

    public IEnumerable<Ticket> FindByEmail(String email)
    {
        return _connectionDatabase.Tickets.Where(ticket => ticket.Email.Equals(email)).ToList();
    }

    public async Task<Ticket> CreateTicket(Ticket ticket) 
    {
        var result =_connectionDatabase.Tickets.Add(ticket);
        await _connectionDatabase.SaveChangesAsync();
        return result.Entity;
    }
}