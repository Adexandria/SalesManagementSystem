using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Model;
using SalesManagementSystem.Services;

namespace SalesManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IGood _good;

        public GoodsController(IGood good)
        {
            _good = good;
        }

        [HttpGet]
        public IActionResult GetAllGoods()
        {
            List<Good> goods = _good.GetGoods;
            return Ok(goods);
        }

        [HttpGet("{goodId}",Name ="GetGood")]
        public IActionResult GetGood(Guid goodId)
        {
            Good good = _good.GetGood(goodId);
            if (good is not null)
            {
                return Ok(good);
            }
            return NotFound();
        }

        [HttpGet("search/name")]
        public IActionResult SearchItemByName(string name)
        {
            List<Good> good = _good.GetGoodByName(name);
            return Ok(good);
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody] Good good)
        {
            _good.CreateGood(good);
            return CreatedAtRoute("GetGood", new { goodId = good.GoodId }, good);

        }

        [HttpPut("{goodId}/name")]
        public IActionResult UpdateGoodByName(Guid goodId, [FromBody] string name)
        {
            Good good = _good.GetGood(goodId);
            if (good is not null)
            {
                _good.UpdateGoodName(goodId, name);
                return Ok("Updated successfully");
            }
            return NotFound();
        }
        
        [HttpPut("{goodId}/Price")]
        public IActionResult UpdateGoodByPrice(Guid goodId, [FromBody] float price)
        {
            Good good = _good.GetGood(goodId);
            if (good is not null)
            {
                _good.UpdateGoodPrice(goodId, price);
                return Ok("Updated successfully");
            }
            return NotFound();
        }

        [HttpPut("{goodId}/Quantity")]
        public IActionResult UpdateGoodByQuantity(Guid goodId, [FromBody] int quantity)
        {
            Good good = _good.GetGood(goodId);
            if (good is not null)
            {
                _good.UpdateGoodQuantity(goodId, quantity);
                return Ok("Updated successfully");
            }
            return NotFound();
        }

        [HttpDelete("{goodId}")]
        public IActionResult DeleteGood(Guid goodId)
        {
            Good good = _good.GetGood(goodId);
            if (good is not null)
            {
                _good.DeleteGood(goodId);
                return Ok("Deleted successfully");
            }
            return NotFound();
        }
    }
}
