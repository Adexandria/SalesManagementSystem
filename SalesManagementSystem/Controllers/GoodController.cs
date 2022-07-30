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

        //Get existing goods
        [HttpGet]
        public IActionResult GetAllGoods()
        {
            List<Good> goods = _good.GetGoods;
            return Ok(goods);
        }

        //Get good by id
        [HttpGet("{goodId}",Name ="GetGood")]
        public IActionResult GetGood(Guid goodId)
        {
            //Get good
            Good good = _good.GetGood(goodId);
            if (good is not null)
            {
                return Ok(good);
            }
            return NotFound();
        }
        

        //Search good by name
        [HttpGet("search/name")]
        public IActionResult SearchGoodByName(string name)
        {
            List<Good> good = _good.GetGoodByName(name);
            return Ok(good);
        }


        //Create new good
        [HttpPost]
        public IActionResult CreateItem([FromBody] Good good)
        {
            _good.CreateGood(good);
            return CreatedAtRoute("GetGood", new { goodId = good.GoodId }, good);

        }

        //Update existing good name
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

        //Update existing good price
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

        ////Update existing good quantity

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
        
        //Delete good by id

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
