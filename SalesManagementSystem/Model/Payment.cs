namespace SalesManagementSystem.Model
{
    public class Payment
    {
        public Payment()
        {
            PaymentId = Guid.NewGuid();
        }
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public float Balance;
    }
}
