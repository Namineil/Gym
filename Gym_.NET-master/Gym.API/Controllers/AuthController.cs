using System.Threading.Tasks;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services;
using Gym.API.Resources;

namespace Gym.API.Controllers
{
    [Route("/api/[controller]")]
    public class AuthController: Controller
    {
        private readonly IAuthService userService;
        private readonly IMapper mapper;

        public AuthController(IAuthService userService, IMapper mapper) {
            this.userService = userService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthorizeUserResource user) {
             var users = await userService.Authenticate(user.Login,
                                                          user.Password, user.RememberMe);
             
             var result = mapper.Map<User, UserResource>(users);
             
             var response = new ResponseData
            {
                Success = result != null,
                Message = result != null ? "" : "Неверный логин или пароль",
                Data = result
            };
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("validate")]
        public async Task<IActionResult> Validate([FromBody] string data) {
            var user = await userService.ValidateToken(data);

             var result = mapper.Map<User, UserResource>(user);
             
             var response = new ResponseData
            {
                Success = result != null,
                Message = result != null ? "" : "Неверный логин или пароль",
                Data = result
            };
            return Ok(response);
        }
    }
}