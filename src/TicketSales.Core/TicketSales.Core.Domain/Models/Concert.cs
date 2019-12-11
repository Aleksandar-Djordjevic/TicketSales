using System;
using CSharpFunctionalExtensions;

namespace TicketSales.Core.Domain.Models
{
    public class Concert
    {
        public string Id { get; }
        public string Name { get; }
        public TicketQuantity SeatingCapacity { get; }
        public int TicketsSold { get; private set; }

        public Concert(string name, TicketQuantity seatingCapacity)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            SeatingCapacity = seatingCapacity;
            TicketsSold = 0;
        }

        public Result<Tickets> IssueTickets(TicketQuantity quantity)
        {
            if (TicketsSold + quantity > SeatingCapacity)
            {
                return Result.Failure<Tickets>(Errors.NoEnoughTickets);
            }

            TicketsSold += quantity;
            return Result.Success(new Tickets(this, quantity));
        }
    }
}
