using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TicketSales.Core.Adapters;
using TicketSales.Core.Application.Ports;
using TicketSales.Core.Application.UseCases.CreateConcert;
using TicketSales.Core.Application.UseCases.SellTickets;
using TicketSales.Core.Domain.Services;
using TicketSales.Core.Web.CommandHandlers;
using TicketSales.Utils.Idempotency;

namespace TicketSales.Core.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConcertRepository, ConcertRepository>();
            services.AddSingleton<IBuyerRepository, BuyerRepository>();
            services.AddTransient<ICreateConcertService, CreateConcertService>();
            services.AddTransient<ISellTicketsService, SellTicketsService>();
            services.AddTransient<IPublishEvents, EventsPublisher>();
            services.AddTransient<ITicketsService, TicketsService>();
            services.AddSingleton<IIdempotencyService, IdempotencyService>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateConcertCommandHandler>();
                x.AddConsumer<SellTicketsCommandHandler>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host("localhost", "tickets", h => { h.Username("guest"); h.Password("guest"); });

                    cfg.ReceiveEndpoint(host, "core", e =>
                    {
                        e.PrefetchCount = 16;

                        e.ConfigureConsumer<CreateConcertCommandHandler>(provider);
                        e.ConfigureConsumer<SellTicketsCommandHandler>(provider);
                    });

                    // or, configure the endpoints by convention
                    cfg.ConfigureEndpoints(provider);
                }));
            });

            services.AddHostedService<BusService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
