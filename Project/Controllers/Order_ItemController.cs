
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Service;
using Project.Model;
using System.Diagnostics.Contracts;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order_ItemsController : ControllerBase
    {
        private readonly IOrder_Item order_ItemRepo;
        private readonly ICart cart;

        public Order_ItemsController(IOrder_Item order_ItemRepo, ICart cart)
        {
            this.order_ItemRepo = order_ItemRepo;
            this.cart = cart;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IEnumerable<Order_Item>> GetOrderedItems(Guid orderId)
        {
            int id = int.Parse(HttpContext.User.FindFirst(x => x.Type == "UserId").Value);
            try
            {
                var res = await order_ItemRepo.GetOrderedItems(orderId, id);
                return res;
            }
            catch (Exception ex)
            {
                //exception handled by a higher-level exception handler that can return an appropriate HTTP response to the client.
                throw;
            }
              
          
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult AddOrderedItems(Guid orderId)
        {
            var id = int.Parse(HttpContext.User.FindFirst(c => c.Type == "UserId").Value);
            try
            {
                order_ItemRepo.AddOrderBillsItem(id, orderId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

        }


        [HttpGet]
        [Route("getCartItems")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetOrderItems()
        {
            try
            {
                int id = int.Parse(HttpContext.User.FindFirst(t => t.Type == "UserId").Value);
                var result = cart.GetAllCartItems(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

        }
       
        [HttpGet("AllOrders")]

        [Authorize(Roles = "Admin")]

        public IActionResult GetOrderedItems()

        {
            try
            {
                var result = order_ItemRepo.GetAllOrders();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }


        }

        [HttpGet("MyOrders")]
        public IActionResult GetMyOrders()
        {
            try
            {
                int id = int.Parse(HttpContext.User.FindFirst(t => t.Type == "UserId").Value);
                var result = order_ItemRepo.GetMyOrders(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

        }
    }
}
