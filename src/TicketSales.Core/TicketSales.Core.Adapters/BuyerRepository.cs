using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TicketSales.Core.Application.Ports;
using TicketSales.Core.Domain.Models;

namespace TicketSales.Core.Adapters
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly List<TicketsBuyer> _buyers;

        public BuyerRepository()
        {
            _buyers = new List<TicketsBuyer> { new TicketsBuyer("1", "Alex")};
        }

        public Task<Maybe<TicketsBuyer>> GetBuyer(string buyerId)
        {
            return Task.FromResult(_buyers.TryFirst(buyer => buyer.Id == buyerId));
        }
    }
}
