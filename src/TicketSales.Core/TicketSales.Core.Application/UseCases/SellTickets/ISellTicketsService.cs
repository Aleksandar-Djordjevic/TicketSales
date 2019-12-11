using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TicketSales.Core.Domain.Models;

namespace TicketSales.Core.Application.UseCases.SellTickets
{
    public interface ISellTicketsService
    {
        Task<Result<Purchase>> SellTickets(SellTicketsRequest request);
    }
}