namespace SalesManagementSystem.Model
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid GoodId { get; set; } 
        public int Quantity { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public float Amount { get; set; }
    }
}
