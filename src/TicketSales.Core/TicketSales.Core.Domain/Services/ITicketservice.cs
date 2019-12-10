using CSharpFunctionalExtensions;
using Domain.Models;

namespace Domain.Services
{
    public interface ITicketsService
    {
        Result<Purchase> SellTickets(Concert concert, TicketsBuyer buyer, TicketQuantity quantity);
    }
}