using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticketservice.Models;
[Table("tickets", Schema = "dbo")]
public class Ticket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ticket_id")]
    public int TicketId { get; set; }
    [Column("title")]
    [Required]
    public required String Title {get; set;}
    [Column("email")]
    [EmailAddress]
    [Required]
    public required String Email {get; set;}
    [Column("create_date")]
    public DateTime CreateDate {get; set;}
    [Column("ticket_state", TypeName = "nvarchar(24)")]
    [Required]
    public TicketState TicketState {get; set;}
    [Column("department")]
    [Required]
    public String Department { get; set; }

    public Ticket()
    {
        CreateDate = DateTime.UtcNow;
    }

}