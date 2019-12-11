namespace TicketSales.Core.Application.UseCases.CreateConcert
{
    public class CreateConcertRequest
    {
        public string Name { get; set; }
        public int SeatingCapacity { get; set; }
    }
}