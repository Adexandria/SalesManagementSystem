namespace SalesManagementSystem.Services
{
    public interface IPayment
    {
        bool IsPaymentValid(Guid orderId);
        bool AddPayment(Guid userId,Guid orderId, float amount);
        bool UpdatePayment(Guid orderId, float amount);
    }
}
