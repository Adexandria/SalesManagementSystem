using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Model;
using SalesManagementSystem.Services;

namespace SalesManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodController : ControllerBase
    {
        private readonly IGood _good;

        public GoodController(IGood good)
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
            if (good is not null)
            {
                return Ok(good);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody] Good good)
        {
            _good.CreateGood(good);
            return CreatedAtRoute("GetGood", new { goodId = good.GoodId }, good);

        }

        [HttpPut("{goodId}")]
        public IActionResult UpdateGoodByName(Guid goodId, string name)
        {
            Good good = _good.GetGood(goodId);
            if (good is not null)
            {
                _good.UpdateGoodName(goodId, name);
                return Ok(good);
            }
            return NotFound();
        }
    }
}
