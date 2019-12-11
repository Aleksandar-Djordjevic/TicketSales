using CSharpFunctionalExtensions;
using TicketSales.Core.Domain.Models;

namespace TicketSales.Core.Domain.Services
{
    public interface ITicketsService
    {
        Result<Purchase> SellTickets(Concert concert, TicketsBuyer buyer, TicketQuantity quantity);
    }
}