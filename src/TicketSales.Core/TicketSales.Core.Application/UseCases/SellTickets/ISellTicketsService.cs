using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.Models;

namespace TicketSales.Core.Application.UseCases.BuyTickets
{
    public interface ISellTicketsService
    {
        Task<Result<Purchase>> SellTickets(SellTicketsRequest request);
    }
}