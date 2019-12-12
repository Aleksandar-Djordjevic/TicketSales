using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TicketSales.User.Models;

namespace TicketSales.User.Services
{
    public interface IStoreConcerts
    {
        Task AddConcert(ConcertToBuy concertToBuy);
        Task<Maybe<ConcertToBuy>> Get(string id);
        Task<List<ConcertToBuy>> GetAll();
    }
}