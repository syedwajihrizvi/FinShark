using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinShark.DTOs.Account;
using FinShark.DTOs.Register;
using FinShark.Interfaces;
using FinShark.Mappers;
using FinShark.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };
                if (registerDto.Password != null)
                {
                    var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                    if (createdUser.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                        if (roleResult.Succeeded)
                        {
                            var token = _tokenService.CreateToken(appUser);
                            return Ok(registerDto.ToCreatedUserDto(token));
                        }
                        else
                        {
                            return StatusCode(500, "Assigning Role to user failed");
                        }
                    }
                    else
                    {
                        return StatusCode(500, createdUser.Errors.First().Description);
                    }
                }
                return BadRequest("Registeration failed due to no password.");
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, $"An exception occured! {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(loginDto.Username!);
            if (user == null)
                return Unauthorized("Please check username or password");
            var result = _signInManager.CheckPasswordSignInAsync(user, loginDto.Password!, false);
            if (result.IsCompletedSuccessfully)
                return Ok(new CreatedUserDto
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                });
            return Unauthorized("Please check username of password");
        }
    }
}