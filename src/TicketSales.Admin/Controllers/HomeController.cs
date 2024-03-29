﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketSales.Admin.Models;
using TicketSales.Admin.Services;
using MassTransit;
using TicketSales.Core.Web.Commands;

namespace TicketSales.Admin.Controllers
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

        // GET: Concerts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Concerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Capacity")] Concert concert)
        {
            if (ModelState.IsValid)
            {
                await _bus.Send(new CreateConcertCommand(Guid.NewGuid().ToString(), concert.Name, concert.Capacity));
                return RedirectToAction(nameof(Index));
            }
            return View(concert);
        }
    }
}
