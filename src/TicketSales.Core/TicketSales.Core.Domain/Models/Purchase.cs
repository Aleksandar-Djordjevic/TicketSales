using System;

namespace TicketSales.Core.Domain.Models
{
    public class Purchase
    {
        public string Id { get; }
        public TicketsBuyer Buyer { get; }
        public Tickets Tickets { get; }

        public Purchase(TicketsBuyer buyer, Tickets tickets)
        {
            Id = Guid.NewGuid().ToString();
            Buyer = buyer;
            Tickets = tickets;
        }
    }
}