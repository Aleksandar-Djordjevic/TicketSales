using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TicketSales.Core.Web.Commands;
using TicketSales.User.Models;
using TicketSales.User.Services;

namespace TicketSales.User.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBus _bus;
        private readonly IStoreConcerts _concertStore;

        public HomeController(IBus bus, IStoreConcerts concertStore)
        {
            _bus = bus;
            _concertStore = concertStore;
        }

        // GET: Concerts
        public async Task<IActionResult> Index()
        {
            return View(await _concertStore.GetAll());
        }

        // GET: Concerts/BuyTickets/5
        public async Task<IActionResult> BuyTickets(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maybeConcert = await _concertStore.Get(id);
            if (maybeConcert.HasValue)
            {
                return View(maybeConcert.Value);
            }

            return NotFound();
        }

        // POST: Concerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyTickets(string id, [Bind("Id,Name,Quantity")] ConcertToBuy concertToBuy)
        {
            if (id != concertToBuy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bus.Send(new SellTicketsCommand(Guid.NewGuid().ToString(), "1", concertToBuy.Id, concertToBuy.Quantity));
                return RedirectToAction(nameof(Index));
            }
            return View(concertToBuy);
        }
    }
}
