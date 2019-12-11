using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TicketSales.Core.Domain.Models;

namespace TicketSales.Core.Application.Ports
{
    public interface IConcertRepository
    {
        Task AddConcert(Concert concert);
        Task<Maybe<Concert>> GetConcert(string concertId);
    }
}
