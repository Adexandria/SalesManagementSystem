using SalesManagementSystem.Model;

namespace SalesManagementSystem.Services
{
    public interface IOrder
    {
        Order GetUserOrder(Guid userId, Guid orderId);
        List<Order> GetUserOrders(Guid userId);
        void CreateOrder(Guid userId,Order order);  
        void UpdateOrderQuantity(Guid orderId, Guid userId, int quantity);
        void UpdateUserOrderStatus(Guid userId, Guid orderId, OrderStatus status);
        void DeleteOrder(Guid orderId,Guid UserId);
    }
}
