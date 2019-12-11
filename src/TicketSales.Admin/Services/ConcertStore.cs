﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TicketSales.Admin.Models;

namespace TicketSales.Admin.Services
{
    public class ConcertStore : IStoreConcerts
    {
        private readonly List<Concert> _concerts;

        public ConcertStore()
        {
            _concerts = new List<Concert>
            {
                new Concert { Id = "1", Name = "My first concert ever", Capacity = 1000, TicketsSold = 0 }
            };
        }

        public Task AddConcert(Concert concert)
        {
            _concerts.Add(concert);
            return Task.CompletedTask;
        }

        public Task<Maybe<Concert>> Get(string id)
        {
            var result = _concerts.TryFirst(concert => concert.Id == id);
            return Task.FromResult(result);
        }

        public Task<List<Concert>> GetAll()
        {
            return Task.FromResult(_concerts);
        }
    }
}
