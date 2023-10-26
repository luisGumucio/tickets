using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Ticketservice.Models;

namespace Ticketservice.Config;

public class ConnectionDatabase : DbContext
{
    public ConnectionDatabase(DbContextOptions<ConnectionDatabase> dbContextOptions) : base(dbContextOptions)
    {
        try
        {
            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (databaseCreator != null)
            {
                if (!databaseCreator.CanConnect()) databaseCreator.Create();
                if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
        .Entity<Ticket>()
        .Property(e => e.TicketState)
        .HasConversion(
            v => v.ToString(),
            v => (TicketState)Enum.Parse(typeof(TicketState), v));
    }
    public DbSet<Ticket> Tickets { get; set; }
}
