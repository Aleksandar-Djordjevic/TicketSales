using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TicketSales.User.Models;

namespace TicketSales.User.Services
{
    public interface IStoreConcerts
    {
        Task AddConcert(Concert concert);
        Task<Maybe<Concert>> Get(string id);
        Task<List<Concert>> GetAll();
        Task UpdateConcert(Concert concert);
    }
}