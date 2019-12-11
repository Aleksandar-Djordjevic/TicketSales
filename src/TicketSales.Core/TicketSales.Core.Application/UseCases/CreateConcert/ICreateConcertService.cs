using System.Threading.Tasks;

namespace TicketSales.Core.Application.UseCases.CreateConcert
{
    public interface ICreateConcertService
    {
        Task CreateConcert(CreateConcertRequest request);
    }
}