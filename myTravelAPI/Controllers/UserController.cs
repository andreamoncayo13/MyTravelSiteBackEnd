using myTravelAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myTravelAPI.Models;
using myTravelAPI.ControllerObjects;

namespace myTravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public UserController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRq userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userObj.UserName && x.Password == userObj.Password);
            if (user == null)
                return NotFound(new { Message = "User Not Found!" });

            return Ok(new
            {
                Message = "Successful Login"
            });

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userObj.UserName);

            if (user != null)
                return BadRequest("User has already been registered.");

            await _dbContext.Users.AddAsync(userObj);

            _dbContext.SaveChanges();

            return Ok(new
            {
                Message = "User Registered!"
            });
        }

    }
}
