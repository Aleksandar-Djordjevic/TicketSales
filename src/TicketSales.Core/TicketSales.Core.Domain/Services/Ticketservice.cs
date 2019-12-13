using CSharpFunctionalExtensions;
using TicketSales.Core.Domain.Models;

namespace TicketSales.Core.Domain.Services
{
    public class TicketsService : ITicketsService
    {
        public Result<Purchase> SellTickets(IConcert concert, TicketsBuyer buyer, TicketQuantity quantity)
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
