using SalesManagementSystem.Model;

namespace SalesManagementSystem.Services
{
    public class OrderRepository : IOrder
    {
        static List<Order> orders = new();
        public void CreateOrder(Guid userId,Order order)
        {
            order.CustomerId = userId;
            order.Amount = GetTotalPrice(order,order.GoodId);
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


        public void UpdateOrderQuantity(Guid orderId,Guid userId, int quantity)
        {
            Order order = GetUserOrder(userId, orderId);
            if(order is not null)
            {
                order.Quantity = quantity;
                order.Amount = GetTotalPrice(order, order.GoodId);
            }
        }

        public void UpdateUserOrderStatus(Guid userId, Guid orderId, OrderStatus status)
        {
            Order order = GetUserOrder(userId, orderId);
            if (order is not null)
            {
                order.OrderStatus = status;
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

       
        
        private float GetTotalPrice(Order order,Guid goodId)
        {
            Good currentGood = GoodRepository.goods.FirstOrDefault(s => s.GoodId == goodId);
            return order.Quantity * currentGood.Price; 
        }
    }
}
