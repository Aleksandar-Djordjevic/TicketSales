namespace TicketSales.Admin.Models
{
    public class Concert
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int TicketsSold { get; set; }
    }
}
