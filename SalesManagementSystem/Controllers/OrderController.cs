using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Model;
using SalesManagementSystem.Services;

namespace SalesManagementSystem.Controllers
{
    [Route("api/{userId}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder _order;
        private readonly IUser _user;
        private readonly IGood _good;

        public OrdersController(IOrder order, IUser user, IGood good)
        {
            _order = order;
            _user = user;
            _good = good;
        }

        [HttpGet]
        public IActionResult GetUserOrders(Guid userId)
        {
            bool isExist = _user.IsUserExist(userId);
            if (isExist)
            {
                List<Order> orders = _order.GetUserOrders(userId);
                return Ok(orders);
            }
            return NotFound("User not found");
        }

        [HttpGet("{orderId}", Name = "GetOrder")]
        public IActionResult GetOrder(Guid userId, Guid orderId)
        {
            bool isExist = _user.IsUserExist(userId);
            if (isExist)
            {
                Order order = _order.GetUserOrder(userId, orderId);
                return Ok(order);
            }
            return NotFound("User not found");
        }

        [HttpPost]
        public IActionResult CreateOrder(Guid userId, [FromBody] Order order)
        {
            bool isExist = _user.IsUserExist(userId);
            Good currentGood = _good.GetGood(order.GoodId);
            if (!isExist)
            {
                return NotFound("User not found");
            }
            if (currentGood is null)
            {
                return NotFound("item not found");
            }
            if(order.Quantity > currentGood.Quantity)
            {
                return BadRequest("This quantity is unavailable");
            }
            _order.CreateOrder(userId, order);
            return CreatedAtRoute("GetOrder", new { userId, orderId = order.OrderId }, order);

        }

        [HttpPut("{orderId}/Quantity")]
        public IActionResult UpdateOrderQuantity(Guid userId, Guid orderId,[FromBody] int quantity)
        {
            bool isExist = _user.IsUserExist(userId);
            if (!isExist)
            {
                return NotFound("User not found");
            }

            Order currentOrder = _order.GetUserOrder(userId, orderId);
            Good currentGood = _good.GetGood(currentOrder.GoodId);
            if (currentOrder.Quantity > currentGood.Quantity)
            {
                return BadRequest("This quantity is unavailable");
            }

            if (currentOrder is not null)
            {
                _order.UpdateOrderQuantity(orderId, userId, quantity);
                return Ok("Updated successfully");
            }
            return NotFound("order not found");
        }

        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(Guid userId, Guid orderId)
        {
            bool isExist = _user.IsUserExist(userId);
            if (!isExist)
            {
                return NotFound("User not found");
            }

            Order currentOrder = _order.GetUserOrder(userId, orderId);
            if (currentOrder is not null)
            {
                _order.DeleteOrder(orderId, userId);
                return Ok("Deleted successfully");

            }
            return NotFound("order not found");
        }
    }
}
