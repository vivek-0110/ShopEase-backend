
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Service;
using Project.Model;

namespace Project.Controllers
{
 
    [Route("api/[controller]")]

    [ApiController]

    public class ProductsController : ControllerBase


    {

        private readonly IProduct productRepo;
        private readonly IMapper mapper;

        public ProductsController(IProduct productRepo,IMapper mapper)

        {

            this.productRepo = productRepo;
            this.mapper = mapper;   

        }

       

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()

        {
            try
            {
                var result = await productRepo.GetAllProducts();


                if (result != null)

                {
                    var resultDto = mapper.Map<List<Product>>(result);

                    return Ok(resultDto);

                }

                return NoContent();

                // return Ok(result);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }


        }

       

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetProductById(int id)

        {
            try
            {
                var product = await productRepo.GetProductById(id);

                if (product == null)

                {

                    return NotFound();

                }

                return product;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }


        }

       

       

        [HttpPut("{id}")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateProduct(int id, Product product)

        {

            try

            {

                await productRepo.UpdateProduct(id, product);

            }

            catch (DbUpdateConcurrencyException)

            {

                if (!ProductExists(id))

                {

                    return NotFound();

                }

                else

                {

                    throw;

                }

            }

            return NoContent();

        }

        [HttpPost]

       [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Product>> AddProduct(Product product)

        {
            try
            {
                var result = await productRepo.AddProduct(product);

                return Ok(result);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }




        }

       

        [HttpDelete("{id}")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteProduct(int id)

        {
            try
            {
                var product = await productRepo.DeleteProduct(id);

                if (product == null)

                {

                    return NotFound();

                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }


        }

        private bool ProductExists(int id)

        {
            try
            {
                return productRepo.ProductExists(id);
            }
            catch(Exception ex)
            {
                throw;
            }
            

        }

        [HttpGet]

        [Route("search/{searchItem}")]

        public async Task<ActionResult<IEnumerable<Product>>> SearchProductByNameOrDesc(string searchItem)

        {
            try
            {
                var result = await productRepo.SearchProduct(searchItem);

                if (result != null)

                {
                    var resultDto = mapper.Map<List<Product>>(result);


                    return Ok(resultDto);

                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }



        }

        [HttpGet("GetAllProductsByCategoryId")]

        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryId(int categoryId)

        {
            try
            {
                var result = await productRepo.GetProductsByCategoryId(categoryId);

                if (result == null)

                {

                    return NotFound();

                }
                var resultDto = mapper.Map<List<Product>>(result);


                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }



        }


    }

}


