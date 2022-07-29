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
        public float Amount { get; set; }
        public float Balance { get; set; }
        public DateTime Date { get; set; }
    }
}
