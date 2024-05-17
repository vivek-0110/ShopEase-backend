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
    public class CategoriesController : ControllerBase

    {

        private readonly IProduct productRepo;
        private readonly IMapper mapper;

        public CategoriesController(IProduct productRepo,IMapper mapper)

        {

            this.productRepo = productRepo;
            this.mapper = mapper;

        }

       

        [HttpGet]


        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()

        {
            try
            {
                var result = await productRepo.GetAllCategories();

                if (result != null)

                {
                    var resultDto = mapper.Map<List<Category>>(result);

                    return Ok(resultDto);

                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }


        }

       

        [HttpGet("{id}")]

        public async Task<ActionResult<Category>> GetCategory(int id)

        {
            try
            {
                var category = await productRepo.GetCategoriesByCategoryId(id);

                if (category == null)

                {

                    return NotFound();

                }

                return category;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }



        }

       

        

        [HttpPut("{id}")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> PutCategory(int id, [FromBody] Category category)

        {

            if (id != category.CategoryId)

            {

                return BadRequest();

            }



            try

            {

                await productRepo.UpdateCategory(id, category);

            }

            catch (DbUpdateConcurrencyException)

            {

                if (!CategoryExists(id))

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

        public async Task<ActionResult<Category>> PostCategory([FromBody] Category category)

        {
            try
            {
                await productRepo.AddCategory(category);

                return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
            }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }



        }

        

        [HttpDelete("{id}")]

      //  [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteCategory(int id)

        {
            try
            {
                var category = await productRepo.DeleteCategory(id);

                if (category == null)

                {

                    return NoContent();

                }


                return Ok("Deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }



        }

        private bool CategoryExists(int id)

        {
            try
            {
                return productRepo.CategoryExists(id);
            }
            catch (Exception ex)
            {
                throw;
            }


        }

    }
}

