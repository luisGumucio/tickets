using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ticketservice.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketState
    {
        OPEN,
        PROCCESSING,
        DONE,
        CANCELED
    }
}