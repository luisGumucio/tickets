using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticketservice.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public required String Title { get; set; }
        public required String Email { get; set; }
        public DateTime CreateDate { get; set; }
        public TicketState TicketState { get; set; }
        public String Department { get; set; }

        public Ticket()
        {
        }

    }
}

