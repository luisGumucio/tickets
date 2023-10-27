using Microsoft.AspNetCore.Mvc;
using Ticketservice.Config;
using Ticketservice.Models;
using Ticketservice.Services;

namespace Ticketservice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketManagmentService;

    public TicketController(ITicketService ticketManagmentService)
    {
        _ticketManagmentService = ticketManagmentService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Ticket>> GetTickets()
    {
        var tickets = _ticketManagmentService.GetAllTickets();
        return Ok(tickets);
    }
    [HttpGet("GetTicketsByEmail")]
    public ActionResult<IEnumerable<Ticket>> GetTicketsByEmail(String email)
    {
        var tickets = _ticketManagmentService.FindByEmail(email);
        if (!tickets.Any()) {
            return NotFound();
        }
        return Ok(tickets);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Ticket ticket)
    {
        await _ticketManagmentService.CreateTicket(ticket);
        return Ok();
    }
}