using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//[Authorize]
public class usersController(DataContext context) : BaseApiController
{
  // private readonly DataContext _context = context;

    [AllowAnonymous]
    [HttpGet]
   public async Task<ActionResult<IEnumerable<AppUser>>>GetUser()
   {
      var user =await context.Users.ToListAsync();
      return Ok(user);
   }
   [Authorize]
   [HttpGet("{id:int}")]
   public async Task<ActionResult<AppUser>> GetUser(int id)
   {
    var user =await context.Users.FindAsync(id);
      if (user == null)
         return NotFound();

      return Ok(user);
   }
}
