using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketSales.Admin.Models;

namespace TicketSales.Admin.Services
{
    public interface IStoreConcerts
    {
        Task AddConcert(Concert concert);
        Task<Maybe<Concert>> Get(string id);
        Task<List<Concert>> GetAll();
    }
}