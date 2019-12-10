using CSharpFunctionalExtensions;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TicketSales.Core.Application.Ports
{
    public interface IBuyerRepository
    {
        Task<Maybe<TicketsBuyer>> GetBuyer(string buyerId);
    }
}
