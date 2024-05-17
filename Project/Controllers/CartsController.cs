
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Infrastructure.Service;
using Project.Model;
using Project.Model.DTO;
using static Project.Infrastructure.Implementation.CartRepository;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase

    {

        private readonly ICart cartRepo;
        private readonly IMapper mapper;
        public CartsController(ICart cartRepo, IMapper mapper)

        {

            this.cartRepo = cartRepo;
            this.mapper = mapper;

        }

        

       [HttpGet]
       [Route("GetCartItems")]
       [Authorize(Roles = "Customer")]

        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()

        {
            try {
                var result = cartRepo.GetAllCartItems(GetIdFromToken());

                if (result.Count > 0)
                {
                    var resultDto = mapper.Map<List<Cart>>(result);
                    return Ok(resultDto);
                }
                return NoContent();
            }

            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

            

        }

       
       /* [HttpPut]
        [Route("UpdateCartItemsDatabase")]
        [Authorize(Roles = "Customer")]*/

        /*public async Task<IActionResult> UpdateCartDatabase([FromBody] ShoppingCartUpdate[] cartItems)

        {

            try

            {

                cartRepo.UpdateCartItemDB(GetIdFromToken(), cartItems);

            }
            catch (Exception ex)

            {

                return BadRequest(ex.Message);

            }

            return Ok(cartItems);

        }
*/
       

        [HttpPost]
        [Route("AddItemToCart")]
        [Authorize(Roles = "Customer")]

        public async Task<ActionResult<Cart>> PostCart(int productId)

        {
            try 
            {
                var result = await cartRepo.AddToCart(productId, GetIdFromToken());
                var resultDto = mapper.Map<CartDto>(result);
                return Ok(resultDto);
            }
           

            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }


        }



        [HttpDelete("RemoveItem/{productId}")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> RemoveItem(int productId)

        {
            try 
            {
                var cart = await cartRepo.RemoveItem(productId, GetIdFromToken());

                if (cart == null)

                {

                    return NotFound();

                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }



        }

        [HttpGet]
        [Route("TotalPrice")]
        public ActionResult<int> GetTotalPrice()
        {
            try
            {
                var result = cartRepo.GetToTalPrice(GetIdFromToken());
                return Ok(result);
            }
            

            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        private int GetIdFromToken()
        {         
            var id = HttpContext.User.FindFirst("userId").Value;
            return int.Parse(id);
        }

        [HttpPut]
        [Route("updateItem")]
        [Authorize(Roles = "Customer")]

        public IActionResult UpdateItem(int productId, int quantity)

        {
            try
            {
                cartRepo.UpdateItem(GetIdFromToken(), productId, quantity);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }


        }

        [HttpDelete]
        [Route("EmptyCart")]
        [Authorize(Roles = "Customer")]

        public IActionResult EmptyCart()

        {
            try 
            {
                cartRepo.EmptyCart(GetIdFromToken());

                return Ok();
            }
            

            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

        }

        [HttpGet]
        [Route("GetCount")]
        [Authorize(Roles = "Customer")]

        public ActionResult<int> TotalCount()

        {
            try 
            {
                var result = cartRepo.GetCount(GetIdFromToken());

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }


        }

    }


}
