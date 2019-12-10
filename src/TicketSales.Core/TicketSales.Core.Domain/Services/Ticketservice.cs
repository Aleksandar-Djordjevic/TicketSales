using CSharpFunctionalExtensions;
using Domain.Models;

namespace Domain.Services
{
    public class TicketsService : ITicketsService
    {
        public Result<Purchase> SellTickets(Concert concert, TicketsBuyer buyer, TicketQuantity quantity)
        {
            var ticketsResult = concert.IssueTickets(quantity);
            return ticketsResult
                .Map(tickets => {
                    buyer.AddPurchase(tickets);
                    return new Purchase(buyer, tickets);
                });
        }
    }
}
