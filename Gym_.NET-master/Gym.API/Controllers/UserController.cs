using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services;
using Gym.API.Resources;
using Gym.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Gym.API.Controllers
{
    [Route("/api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuthService authService;
        private readonly IMapper mapper;
        public UserController(IUserService userService, IAuthService authService, IMapper mapper)
        {
            this.userService = userService;
            this.authService = authService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserResource>> GetAllAsync() 
        {
            var user = await userService.ListAsync();
            var resource = mapper.Map<IEnumerable<User>,IEnumerable<UserResource>>(user);
            return resource;
        }
        
        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = mapper.Map<SaveUserResource, User>(resource);
            user.Password = this.authService.hashPwd(user.Password);
            var result = await userService.SaveAsync(user);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var u = await this.authService.Authenticate(user.Login, resource.Password, false);
            var userResource = mapper.Map<User, UserResource>(u);
            var response = new ResponseData
            {
                Success = userResource != null,
                Message = userResource != null ? "" : "Неверный логин или пароль",
                Data = userResource
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = mapper.Map<SaveUserResource, User>(resource);
            var result = await userService.UpdateAsync(id, user);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var userResource = mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

        [HttpPut("pass/{id}")]
        public async Task<IActionResult> PutPassAsync(int id, [FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            resource.Password = this.authService.hashPwd(resource.Password);
            var user = mapper.Map<SaveUserResource, User>(resource);
            var result = await userService.UpdateAsync(id, user);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var userResource = mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await userService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            var userResource = mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }
    }
}