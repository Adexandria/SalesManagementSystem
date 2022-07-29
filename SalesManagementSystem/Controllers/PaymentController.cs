using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Model;
using SalesManagementSystem.Services;

namespace SalesManagementSystem.Controllers
{
    [Route("api/{userId}/{orderId}/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _payment;
        private readonly IOrder _order;
        private readonly IUser _user;


        public PaymentController(IPayment payment, IOrder order, IUser user)
        {
            _payment = payment;
            _order = order;
            _user = user;
        }

        [HttpPost("Pay")]
        public IActionResult Pay(Guid userId, Guid orderId, [FromBody] float amount)
        {
            bool isExist = _user.IsUserExist(userId);
            if (!isExist)
            {
                return NotFound("User not found");
            }
            Order currentOrder = _order.GetUserOrder(userId, orderId);
            if (currentOrder is null)
            {
                return NotFound("order not found");
            }
            bool isValid = _payment.IsPaymentValid(orderId);
            if (isValid)
            {
               return BadRequest("This order has been paid for");
            }

            bool isSuccessful = _payment.AddPayment(userId, orderId, amount);
            if (isSuccessful)
            {
               _order.UpdateUserOrderStatus(userId, orderId, OrderStatus.Paid);
               return Ok("Payment successful");
            }
            _order.UpdateUserOrderStatus(userId, orderId, OrderStatus.NotPaid);
            return BadRequest("Payment failed");
            
        }

        [HttpPut]
        public IActionResult UpdatePayment(Guid userId,Guid orderId, [FromBody] float amount)
        {
            bool isExist = _user.IsUserExist(userId);
            if (!isExist)
            {
                return NotFound("User not found");
            }
            Order currentOrder = _order.GetUserOrder(userId, orderId);
            if (currentOrder is null)
            {
                return NotFound("order not found");
            }
            bool isValid = _payment.IsPaymentValid(orderId);
            if (!isValid)
            {
                return BadRequest("This order hasn't been paid yet");
            }

            bool isSuccessful = _payment.UpdatePayment(orderId,amount);
            if (isSuccessful)
            {
                _order.UpdateUserOrderStatus(userId, orderId, OrderStatus.Paid);
                return Ok("Payment successful");
            }
            _order.UpdateUserOrderStatus(userId, orderId, OrderStatus.NotPaid);
            return BadRequest("Payment failed");
        }
    }
}
