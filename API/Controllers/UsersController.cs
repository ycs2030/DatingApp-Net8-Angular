using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Route("api/[Controller]")]
public class usersController(DataContext context) : ControllerBase
{
  // private readonly DataContext _context = context;

    [HttpGet]
   public async Task<ActionResult<IEnumerable<AppUser>>>GetUser()
   {
      var user =await context.Users.ToListAsync();
      return Ok(user);
   }
   [HttpGet("{id:int}")]
   public async Task<ActionResult<AppUser>> GetUser(int id)
   {
    var user =await context.Users.FindAsync(id);
      if (user == null)
         return NotFound();

      return Ok(user);
   }
}
