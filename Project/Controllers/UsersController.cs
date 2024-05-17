
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Infrastructure.Service;
using Project.Model;
using Project.Model.DTO;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase

    {

        private readonly IUser userInterface;
        private readonly IMapper mapper;    

        public UserController(IUser userInterface, IMapper mapper)

        {

            this.userInterface = userInterface;
            this.mapper = mapper;
        }



        [HttpGet]

       

        public async Task<ActionResult<User>> GetAllUsers()

        {
            try 
            {
                var result = await userInterface.GetAll();

                if (result != null)

                {
                    var resultDto = mapper.Map<List<UserDto>>(result);

                    return Ok(resultDto);

                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }


        }

        [HttpPut("{id}")]

        [Authorize(Roles = "Customer")]

        public async Task<ActionResult<User>> UpdateUser([FromBody] User user)

        {

            try
            {
                int id = int.Parse(HttpContext.User.FindFirst("userId").Value);

                var result = await userInterface.UpdateUserDetail(id, user);

                if (result != null)

                {



                    return Ok(result);

                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }



        }

        [HttpPost("login")]

        public ActionResult<LoginResponseDto> Login(LoginRequestDto loginDto)

        {
            try
            {
                var result = userInterface.Login(loginDto);

                if (result != null)

                {

                    return Ok(result);

                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }


        }

        [HttpPost]

        public async Task<ActionResult<User>> AddUser([FromBody] User user)

        {
            try
            {
                var result = await userInterface.AddUserAsync(user);

                if (result != null)

                {

                    return Ok(result);

                }

                return BadRequest("User already exist.Please login");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }


        }


    }
}



