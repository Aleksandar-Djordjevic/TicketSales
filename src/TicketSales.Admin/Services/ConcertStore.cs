using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TicketSales.Admin.Models;

namespace TicketSales.Admin.Services
{
    public class ConcertStore : IStoreConcerts
    {
        private readonly List<Concert> _concerts = new List<Concert>();

        public void AddConcert(Concert concert)
        {
            _concerts.Add(concert);
        }

        public Maybe<Concert> Get(string id)
        {
            return _concerts.TryFirst(concert => concert.Id == id);
        }

        public List<Concert> GetAll()
        {
            return _concerts;
        }
    }
}
