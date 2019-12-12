using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TicketSales.User.Models;

namespace TicketSales.User.Services
{
    public class ConcertStore : IStoreConcerts
    {
        private readonly List<ConcertToBuy> _concerts;

        public ConcertStore()
        {
            _concerts = new List<ConcertToBuy>();
        }

        public Task AddConcert(ConcertToBuy concertToBuy)
        {
            _concerts.Add(concertToBuy);
            return Task.CompletedTask;
        }

        public Task<Maybe<ConcertToBuy>> Get(string id)
        {
            var result = _concerts.TryFirst(concert => concert.Id == id);
            return Task.FromResult(result);
        }

        public Task<List<ConcertToBuy>> GetAll()
        {
            return Task.FromResult(_concerts.ToList());
        }
    }
}
