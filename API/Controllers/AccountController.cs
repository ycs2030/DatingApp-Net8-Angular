using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
    {
        private readonly DataContext _context=context;
        private readonly ITokenService _tokenService=tokenService;

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.Username))
              return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();

            var user = new AppUser{
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users
            .FirstOrDefaultAsync(x => x.UserName
            == loginDto.Username.ToLower());

            if(user == null) 
            return Unauthorized("Invalid username or password :(");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) 
                return Unauthorized("Invalid password or username");
            }
            return new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
 
        private async Task<bool> UserExists(string username)
        {
            // Check if user already exists
            return await _context.Users.
            AnyAsync(x => x.UserName.ToLower()
             == username.ToLower());
        }
    }
