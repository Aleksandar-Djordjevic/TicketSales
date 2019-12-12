using System.ComponentModel.DataAnnotations;

namespace TicketSales.User.Models
{
    public class ConcertToBuy
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Quantity { get; set; }
    }
}
