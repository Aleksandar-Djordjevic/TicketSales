using CSharpFunctionalExtensions;
using System.Collections.Generic;
using TicketSales.Admin.Models;

namespace TicketSales.Admin.Services
{
    public interface IStoreConcerts
    {
        void AddConcert(Concert concert);
        Maybe<Concert> Get(string id);
        List<Concert> GetAll();
    }
}