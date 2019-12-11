using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketSales.User.Services;

namespace TicketSales.User.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly IStorePurchases _purchaseStore;

        public PurchasesController(IStorePurchases purchaseStore)
        {
            _purchaseStore = purchaseStore;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            return View(await _purchaseStore.GetAllForBuyer("1"));
        }
    }
}
