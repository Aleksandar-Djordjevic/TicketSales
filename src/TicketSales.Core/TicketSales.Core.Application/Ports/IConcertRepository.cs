using CSharpFunctionalExtensions;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports
{
    public interface IConcertRepository
    {
        Task AddConcert(Concert concert);
        Task<Maybe<Concert>> GetConcert(string concertId);
    }
}
