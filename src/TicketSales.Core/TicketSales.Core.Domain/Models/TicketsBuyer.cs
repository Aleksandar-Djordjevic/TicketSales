using System.Collections.Generic;

namespace TicketSales.Core.Domain.Models
{
    public class TicketsBuyer
    {
        public string Id { get; }
        public string Name { get; }
        public List<Tickets> Purchases { get; }

        public TicketsBuyer(string id, string name)
        {
            Id = id;
            Name = name;
            Purchases = new List<Tickets>();
        }

        public void AddPurchase(Tickets tickets)
        {
            Purchases.Add(tickets);
        }
    }
}
