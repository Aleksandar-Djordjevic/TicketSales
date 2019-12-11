using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TicketSales.User.Models;

namespace TicketSales.User.Services
{
    public interface IStorePurchases
    {
        Task<Maybe<Purchase>> Get(string id);
        Task<List<Purchase>> GetAllForBuyer(string buyerId);
        Task Add(Purchase purchase);
        Task UpdateStatus(string purchaseId, string status);
    }
}
