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
        private readonly IGood _good;


        public PaymentController(IPayment payment, IOrder order, IUser user, IGood good)
        {
            _payment = payment;
            _order = order;
            _user = user;
            _good = good;
        }


        //Get existing payment
        [HttpGet]
        public IActionResult Getpayment(Guid userId,Guid orderId)
        {
            //Check if the user exist,if false returns not found
            bool isExist = _user.IsUserExist(userId);
            if (!isExist)
            {
                return NotFound("User not found");
            }
            
            //Get existing user Order, if null returns Not found
            Order currentOrder = _order.GetUserOrder(userId, orderId);
            if (currentOrder is null)
            {
                return NotFound("order not found");
            }

            //Get existing order payment
            Payment currentPayment = _payment.GetOrderPayment(orderId);
            if(currentPayment is null)
            {
                return NotFound();
            }
            
            return Ok(currentPayment);
        }
        
        //Add new payment for existing order
        [HttpPost]
        public IActionResult Pay(Guid userId, Guid orderId, [FromBody] float amount)
        {
            //Check if the user exist,if false returns not found
            bool isExist = _user.IsUserExist(userId);
            if (!isExist)
            {
                return NotFound("User not found");
            }

            //Check if the user exist,if false returns not found
            Order currentOrder = _order.GetUserOrder(userId, orderId);
            if (currentOrder is null)
            {
                return NotFound("order not found");
            }

            //Check if the order payment has been made, if true return a bad request
            bool isValid = _payment.IsPaymentValid(orderId);
            if (isValid)
            {
               return BadRequest("This order has been paid for");
            }

            // Add Payment, if successful update order status and good quantity
            bool isSuccessful = _payment.AddPayment(userId, orderId, amount);
            if (isSuccessful)
            {
               _order.UpdateUserOrderStatus(userId, orderId, OrderStatus.Paid);
               _good.UpdateOrderGoodQuantity(currentOrder.GoodId, currentOrder.Quantity);
               return Ok("Payment successful");
            }
            
            return BadRequest("Payment failed");
            
        }

        //Update existing payment for existing order
        [HttpPut]
        public IActionResult UpdatePayment(Guid userId,Guid orderId, [FromBody] float amount)
        {
            //Check if the user exist,if false returns not found
            bool isExist = _user.IsUserExist(userId);
            if (!isExist)
            {
                return NotFound("User not found");
            }

            //Get existing user Order, if null returns Not found
            Order currentOrder = _order.GetUserOrder(userId, orderId);
            if (currentOrder is null)
            {
                return NotFound("order not found");
            }

            //Check if the order payment has been made, if false retun a bad request
            bool isValid = _payment.IsPaymentValid(orderId);
            if (!isValid)
            {
                return BadRequest("This order hasn't been paid for");
            }

            // Update existing payment, update order status
            bool isSuccessful = _payment.UpdatePayment(orderId,amount);
            if (isSuccessful)
            {
                _order.UpdateUserOrderStatus(userId, orderId, OrderStatus.Paid);
                return Ok("Payment successful");
            }
            
            return BadRequest("Payment failed");
        }
    }
}
