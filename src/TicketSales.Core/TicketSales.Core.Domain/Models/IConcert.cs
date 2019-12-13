using CSharpFunctionalExtensions;

namespace TicketSales.Core.Domain.Models
{
    public interface IConcert
    {
        string Id { get; }
        string Name { get; }
        TicketQuantity SeatingCapacity { get; }
        int TicketsSold { get; }

        Result<Tickets> IssueTickets(TicketQuantity quantity);
    }
}