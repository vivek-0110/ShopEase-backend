
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Service;
using Project.Model;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder order;

        public OrdersController(IOrder order)
        {
            this.order = order;
        }

        [HttpPost]
        [Route("AddOrder")]
        public IActionResult BuyNow()
        {

            try
            {
                var result=order.BuyNow(GetIdFromToken());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getNetAmount")]
        public int GetNetAmount()
        {

            var result = order.GetNetAmount(GetIdFromToken());
            return result;
        }

        private int GetIdFromToken()
        {
            var id = HttpContext.User.FindFirst("userId").Value;
            return int.Parse(id);
        }

        [HttpGet]
        [Route("getTotalAmount")]
        public int GetTotalAmount()
        {
            var result = order.GetTotalAmount(GetIdFromToken());
            return result;
        }
    }
}


