namespace TicketSales.Admin.Services
{
    public class TestMessageStore
    {
        private int _count;

        public int GetCount() => _count;

        public void IncrementCount() => _count++;
    }
}
