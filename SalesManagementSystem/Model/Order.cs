namespace SalesManagementSystem.Model
{
    public class Order
    {
        public Order()
        {
            OrderId = Guid.NewGuid();
        }
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public List<Good> Goods { get; set; } = new();
        public int Quantity { get; set; }
    }
}
