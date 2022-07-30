using SalesManagementSystem.Model;

namespace SalesManagementSystem.Services
{
    public class OrderRepository : IOrder
    {
        public static List<Order> orders = new();
        
        //Create new order
        public void CreateOrder(Guid userId,Order order)
        {
            order.OrderId = Guid.NewGuid();
            order.CustomerId = userId;
            order.Amount = GetTotalPrice(order,order.GoodId);
            orders.Add(order);
        }

        //Get user order by order Id

        public Order GetUserOrder(Guid userId, Guid orderId)
        {
            Order order = orders.FirstOrDefault(s => s.CustomerId == userId && s.OrderId == orderId);
            return order;
        }

        //Get user orders by user Id
        public List<Order> GetUserOrders(Guid userId)
        {
            return orders.Where(s => s.CustomerId == userId).ToList();
        }


        //Update order quantity
        public void UpdateOrderQuantity(Guid orderId,Guid userId, int quantity)
        {
            Order order = GetUserOrder(userId, orderId);
            if(order is not null)
            {
                order.Quantity = quantity;
                order.Amount = GetTotalPrice(order, order.GoodId);
            }
        }

        //Update order status
        public void UpdateUserOrderStatus(Guid userId, Guid orderId, OrderStatus status)
        {
            Order order = GetUserOrder(userId, orderId);
            if (order is not null)
            {
                order.OrderStatus = status;
            }
        }

        //Delete order
        public void DeleteOrder(Guid orderId,Guid userId)
        {
            Order order = GetUserOrder(userId, orderId);
            if(order is not null)
            {
                orders.Remove(order);
            }
        }

       
        //Get total price of the order
        private float GetTotalPrice(Order order,Guid goodId)
        {
            Good currentGood = GoodRepository.goods.FirstOrDefault(s => s.GoodId == goodId);
            return order.Quantity * currentGood.Price; 
        }
    }
}
