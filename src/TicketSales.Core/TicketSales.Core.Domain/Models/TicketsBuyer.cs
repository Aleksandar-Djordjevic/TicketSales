using System.Collections.Generic;

namespace Domain.Models
{
    public class TicketsBuyer
    {
        public string Name { get; }
        public List<Tickets> Purchases { get; }

        public TicketsBuyer(string name)
        {
            Name = name;
            Purchases = new List<Tickets>();
        }

        public void AddPurchase(Tickets tickets)
        {
            Purchases.Add(tickets);
        }
    }
}
