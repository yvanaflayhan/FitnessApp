using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly DataContext _context; //store connection to database 
        public UsersController(DataContext context)
        {
            _context = context;
            
        }//this is dependency injection , in Program.cs:builder.Services.AddDbContext<DataContext>(...);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();//User Table , ToList: execute the query 

            return users;
        }

        [HttpGet("{id}")] //GET /api/users/5
        public async Task<ActionResult<AppUser>> GetUser(int id)//return one user 
        {
            return await _context.Users.FindAsync(id); //this tells Entity FrameWork Core: find the user whose primary key equals id 
        }
    }
}
