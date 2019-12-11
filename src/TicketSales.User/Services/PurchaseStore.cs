using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TicketSales.User.Models;

namespace TicketSales.User.Services
{
    public class PurchaseStore : IStorePurchases
    {
        private readonly List<Purchase> _purchases;

        public PurchaseStore()
        {
            _purchases = new List<Purchase>();
        }

        public Task<Maybe<Purchase>> Get(string id)
        {
            var purchase = _purchases.TryFirst(pur => pur.Id == id);
            return Task.FromResult(purchase);
        }

        public Task<List<Purchase>> GetAllForBuyer(string buyerId)
        {
            var result = _purchases.Where(purchase => purchase.BuyerId == buyerId).ToList();
            return Task.FromResult(result);
        }

        public Task Add(Purchase purchase)
        {
            _purchases.Add(purchase);
            return Task.CompletedTask;
        }

        public Task UpdateStatus(string purchaseId, string status)
        {
            var result = _purchases.TryFirst(pur => pur.Id == purchaseId);
            result.Execute(pur => pur.Status = status);
            return Task.CompletedTask;
        }
    }
}
