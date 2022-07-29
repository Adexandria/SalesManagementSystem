using SalesManagementSystem.Model;

namespace SalesManagementSystem.Services
{
    public class PaymentRepository : IPayment
    {
        List<Payment> payments = new();
        
        public bool AddPayment(Guid userId,Guid orderId, float amount)
        {
            Order currentOrder = OrderRepository.orders.FirstOrDefault(s => s.OrderId == orderId && s.CustomerId == userId);
            if (currentOrder is not null)
            {
                Payment payment = new Payment
                {
                    OrderId = orderId,
                    Balance = currentOrder.Amount - amount
                };
                payments.Add(payment);
                return true;
            }
            return false;
        }

        public Payment GetOrderPayment(Guid orderId)
        {
            Payment payment = payments.FirstOrDefault(s => s.OrderId == orderId);
            return payment;
        }

        public bool IsPaymentValid(Guid orderId)
        {
            Payment currentPayment = payments.FirstOrDefault(s => s.OrderId == orderId);
            if(currentPayment is not null)
            {
                return true;
            }
            return false;
        }

        public bool UpdatePayment(Guid orderId, float amount)
        {
            Payment currentPayment = GetOrderPayment(orderId);
            if (currentPayment is not null)
            {
                currentPayment.Balance -= amount;
                return true;
            }
            return false;
        }
    }
}
