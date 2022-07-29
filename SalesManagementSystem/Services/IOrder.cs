using SalesManagementSystem.Model;

namespace SalesManagementSystem.Services
{
    public interface IOrder
    {
        Order GetUserOrder(Guid userId, Guid orderId);
        List<Order> GetUserOrders(Guid userId);
        void CreateOrder(Order order);  
        void UpdateOrderItemQuantity(Guid orderId, Guid userId, Guid itemId, int quantity);
        void UpdateOrderQuantity(Guid orderId, Guid userId, int quantity);
        void DeleteOrder(Guid orderId,Guid UserId);
    }
}
