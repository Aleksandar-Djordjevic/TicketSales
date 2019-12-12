using System;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketSales.Admin.Consumers;
using TicketSales.Admin.Services;
using TicketSales.Core.Web.Commands;

namespace TicketSales.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<IStoreConcerts, ConcertStore>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ConcertCreatedEventHandler>();
                x.AddConsumer<PurchaseSuccessfullyMadeEventHandler>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host("localhost", "tickets", h => { h.Username("guest"); h.Password("guest"); });

                    cfg.ReceiveEndpoint(host, "admin", e =>
                    {
                        e.PrefetchCount = 16;

                        e.ConfigureConsumer<ConcertCreatedEventHandler>(provider);
                        e.ConfigureConsumer<PurchaseSuccessfullyMadeEventHandler>(provider);

                        EndpointConvention.Map<CreateConcertCommand>(new Uri("rabbitmq://localhost/tickets/core"));
                    });

                    // or, configure the endpoints by convention
                    cfg.ConfigureEndpoints(provider);
                }));
            });

            services.AddHostedService<BusService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
