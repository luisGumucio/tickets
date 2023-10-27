using Ticketservice.Config;
using Ticketservice.Models;
using Ticketservice.Producers;
using static MassTransit.ValidationResultExtensions;

namespace Ticketservice.Services;

public class TicketManagmentservice : ITicketService
{
    private readonly ConnectionDatabase _connectionDatabase;
    private readonly ITicketProducer _ticketProducer;

    public TicketManagmentservice(ConnectionDatabase connectionDatabase,
    ITicketProducer ticketProducer)
    {
        _connectionDatabase = connectionDatabase;
        _ticketProducer = ticketProducer;
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
        _ticketProducer.addNewTicket(result.Entity);
        return result.Entity;
    }

    public void UpdateTicket(Ticket ticket)
    {
        var result = _connectionDatabase.Tickets.Where(b => b.TicketId == ticket.TicketId).First();
        result.TicketState = ticket.TicketState;
        _connectionDatabase.SaveChanges();
        _ticketProducer.addNewTicket(result);
    }
}