using SalesManagementSystem.Model;

namespace SalesManagementSystem.Services
{
    public class OrderRepository : IOrder
    {
        List<Order> orders = new();
        public void CreateOrder(Order order)
        {
            orders.Add(order);
        }

   
        public Order GetUserOrder(Guid userId, Guid orderId)
        {
            Order order = orders.FirstOrDefault(s => s.CustomerId == userId && s.OrderId == orderId);
            return order;
        }

        public List<Order> GetUserOrders(Guid userId)
        {
            return orders.Where(s => s.CustomerId == userId).ToList();
        }

        public void UpdateOrderItemQuantity(Guid orderId, Guid userId, Guid itemId, int quantity)
        {
            Order order = GetUserOrder(userId, orderId);
            Good good = order.Goods.FirstOrDefault(x=>x.GoodId == itemId);
            if(good is not null)
            {
                good.Quantity = quantity;
            }
        }

        public void UpdateOrderQuantity(Guid orderId,Guid userId, int quantity)
        {
            Order order = GetUserOrder(userId, orderId);
            if(order is not null)
            {
                order.Quantity = quantity;
            }
        }

        public void DeleteOrder(Guid orderId,Guid userId)
        {
            Order order = GetUserOrder(userId, orderId);
            if(order is not null)
            {
                orders.Remove(order);
            }
        }

    }
}
