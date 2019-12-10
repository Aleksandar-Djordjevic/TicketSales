using Application.Ports;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Application.Adapters
{
    public class ConcertRepository : IConcertRepository
    {
        private readonly List<Concert> _concerts;

        public ConcertRepository()
        {
            _concerts = new List<Concert>();
        }

        public Task AddConcert(Concert concert)
        {
            _concerts.Add(concert);
            return Task.CompletedTask;
        }

        public Task<Maybe<Concert>> GetConcert(string concertId)
        {
            var concert = _concerts.TryFirst(con => con.Id == concertId);
            return Task.FromResult(concert);
        }
    }
}
