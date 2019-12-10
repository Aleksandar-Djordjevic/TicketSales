using System;
using System.Diagnostics;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TicketSales.Admin.Models;
using TicketSales.Admin.Services;
using TicketSales.Messages.Commands;

namespace TicketSales.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBus _bus;
        private readonly TestMessageStore _store;

        public HomeController(IBus bus, TestMessageStore store)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _store = store;
        }

        public IActionResult Index()
        {
            int count = _store.GetCount();
            return View(count);
        }

        [HttpPost, ActionName("Index")]
        public IActionResult IndexPost()
        {
            _bus.Send(new TestCommand());
            int count = _store.GetCount();
            return View(count);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
