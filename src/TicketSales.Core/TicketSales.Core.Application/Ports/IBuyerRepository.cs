using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using TicketSales.Core.Domain.Models;

namespace TicketSales.Core.Application.Ports
{
    public interface IBuyerRepository
    {
        Task<Maybe<TicketsBuyer>> GetBuyer(string buyerId);
    }
}
