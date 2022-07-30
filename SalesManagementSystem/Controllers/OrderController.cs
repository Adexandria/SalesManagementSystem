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

        //Get existing user orders by userId
        [HttpGet]
        public IActionResult GetUserOrders(Guid userId)
        {
            //Check if the user exist,if false returns not found
            bool isExist = _user.IsUserExist(userId);
            if (isExist)
            {
                List<Order> orders = _order.GetUserOrders(userId);
                return Ok(orders);
            }
            return NotFound("User not found");
        }


        //Get existing user order by orderId and userId
        [HttpGet("{orderId}", Name = "GetOrder")]
        public IActionResult GetOrder(Guid userId, Guid orderId)
        {
            //Check if the user exist,if false returns not found
            bool isExist = _user.IsUserExist(userId);
            if (isExist)
            {
                Order order = _order.GetUserOrder(userId, orderId);
                return Ok(order);
            }
            return NotFound("User not found");
        }

        //Create  new user order
        [HttpPost]
        public IActionResult CreateOrder(Guid userId, [FromBody] Order order)
        {
            //Check if the user exist,if false returns not found
            bool isExist = _user.IsUserExist(userId);
            if (!isExist)
            {
                return NotFound("User not found");
            }
            
            //Get existing good by id, if null returns Not found
            Good currentGood = _good.GetGood(order.GoodId);
            if (currentGood is null)
            {
                return NotFound("item not found");
            }
            
            //If the quantity is less than the quantity in stock return not found
            if (order.Quantity > currentGood.Quantity)
            {
                return BadRequest("This quantity is unavailable");
            }
            
            _order.CreateOrder(userId, order);
            return CreatedAtRoute("GetOrder", new { userId, orderId = order.OrderId }, order);

        }

        //Update existing order quantity
        [HttpPut("{orderId}/Quantity")]
        public IActionResult UpdateOrderQuantity(Guid userId, Guid orderId,[FromBody] int quantity)
        {
            //Check if the user exist,if false returns not found
            bool isExist = _user.IsUserExist(userId);
            if (!isExist)
            {
                return NotFound("User not found");
            }
            
            //Get User Order, if null returns Not found
            Order currentOrder = _order.GetUserOrder(userId, orderId);
            if(currentOrder is null)
            {
                return NotFound("item not found");
            }
            
            //Get current good, if null returns Not found
            Good currentGood = _good.GetGood(currentOrder.GoodId);
            if (currentGood is null)
            {
                return NotFound("item not found");
            }
            
            //If the quantity is less than the quantity in stock return not found
            if (currentOrder.Quantity > currentGood.Quantity)
            {
                return BadRequest("This quantity is unavailable");
            }
            
            _order.UpdateOrderQuantity(orderId, userId, quantity);
            return Ok("Updated successfully");
        }


        //Delete Order by orderId
        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(Guid userId, Guid orderId)
        {
            //Check if the user exist,if false returns not found
            bool isExist = _user.IsUserExist(userId);
            if (!isExist)
            {
                return NotFound("User not found");
            }

            //Get User Order, if null returns Not found
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
