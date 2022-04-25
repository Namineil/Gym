using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services;
using Gym.API.Resources;

namespace Shop.API.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    public class UserRoleController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserRoleService userRoleService;

        public IMapper Mapper => mapper;

        public UserRoleController(IMapper mapper, IUserRoleService userRoleService)
        {
            this.mapper = mapper;
            this.userRoleService = userRoleService;
        }

        [Authorize(Roles="admin, user")]
        [HttpGet("{id}")]
        public async Task<ResponseData> ListUserByRoleAsync(int id) 
        {
            var users = await userRoleService.ListUserByRoleAsync(id);
            var userResource = Mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            var result = new ResponseData
            {
                Data = userResource,
                Success = true,
                Message = ""
            };
            return result;
        }

        [Authorize(Roles="admin")]
        [HttpPost]
        public async Task<ResponseData> SetUserRole([FromBody] SaveUserRoleResource resource)
        {
            var userResponse = await userRoleService.SetRole(resource.IdUser, resource.IdRole);
            var userResource = Mapper.Map<User, UserResource>(userResponse.User);
            var result = new ResponseData
            {
                Success = true,
                Message = "",
                Data = userResource
            };
            return result;
        }
    }
}