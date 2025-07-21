using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiApp.Data;
using MyApiApp.Dtos.User;
using MyApiApp.Interfaces;
using MyApiApp.Mappers;
using MyApiApp.Models;

namespace MyApiApp.Controllers
{
    [Route("/api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<Users> _user;
        private readonly ITokenServices _tokenServices;
        private readonly SignInManager<Users> _signInManager;
        private readonly IUsersRepository _usersRepository;

        public UsersController(UserManager<Users> user, ITokenServices tokenServices, SignInManager<Users> signInManager, IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _user = user;
            _tokenServices = tokenServices;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _user.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Invalid Username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenServices.CreateToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new Users
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var createdUser = await _user.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _user.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenServices.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _usersRepository.GetAllAsync();
            var usersDto = users.Select(s => s.ToUsersDto());
            return Ok(usersDto);
        }

    }
}