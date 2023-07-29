using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsersController : Controller
    {
        private readonly UserAPIDbContext _userAPIDbContext;
        public UsersController(UserAPIDbContext userAPIDbContext)
        {
            _userAPIDbContext = userAPIDbContext;
        }    

        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userAPIDbContext.Users.ToListAsync());
            
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await _userAPIDbContext.Users.FindAsync(id);
            if (user != null)
            {

                return Ok(user);    
            }
            else
            {
                return NotFound();
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Address = addUserRequest.Address,
                Email = addUserRequest.Email,
                Phone = addUserRequest.Phone,
                FullName = addUserRequest.FullName
            };
            await _userAPIDbContext.Users.AddAsync(user);
            await _userAPIDbContext.SaveChangesAsync();
            return Ok(user);   
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id,UpdateUserRequest updateUserRequest)
        {
            var user = _userAPIDbContext.Users.Find(id);
            if (user != null) 
            {
                user.FullName = updateUserRequest.FullName;
                user.Address = updateUserRequest.Address;
                user.Phone = updateUserRequest.Phone;
                user.Email  = updateUserRequest.Email;
                await _userAPIDbContext.SaveChangesAsync();


                return Ok(user); 
            }
            return NotFound();

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await _userAPIDbContext.Users.FindAsync(id);
            if (user != null)
            {
                _userAPIDbContext.Remove(user);
                await _userAPIDbContext.SaveChangesAsync();
                return Ok(user);    
            }
            return NotFound();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            var users =  _userAPIDbContext.Users.ToListAsync();

            _userAPIDbContext.RemoveRange(users);
            _userAPIDbContext.SaveChanges();
            return Ok(users);
        }
    }
}
