using System;

namespace TicketSales.Core.Domain.Models
{
    public class TicketQuantity
    {
        public int Value { get; }

        public TicketQuantity(int quantity)
        {
            if (quantity < 1)
                throw new ArgumentException("Quantity must be positive number");

            Value = quantity;
        }

        public static implicit operator int(TicketQuantity quantity) => quantity.Value;
    }
}
