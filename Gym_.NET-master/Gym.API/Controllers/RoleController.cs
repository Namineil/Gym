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
    [Authorize]
    [Route("/api/role")]
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<ResponseData> GetAllAsync()
        {
            var role = await roleService.ListAsync();
            var resource = mapper.Map<IEnumerable<Role>, IEnumerable<RoleResource>>(role);
            var result = new ResponseData
            {
                Data = resource,
                Message = "",
                Success = true
            };
            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveRoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var role = mapper.Map<SaveRoleResource, Role>(resource);
            var roleResponse = await roleService.SaveAsync(role);
            var roleResource = mapper.Map<Role, RoleResource>(roleResponse.Role);
            var result = new ResponseData {
                Data = roleResource,
                Message = roleResponse.Message,
                Success = roleResponse.Success
            };
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var role = mapper.Map<SaveRoleResource, Role>(resource);
            var roleResponse = await roleService.UpdateAsync(id, role);
            var roleResource = mapper.Map<Role, RoleResource>(roleResponse.Role);
            var result = new ResponseData {
                Data = roleResource,
                Message = roleResponse.Message,
                Success = roleResponse.Success
            };
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var roleResponse = await roleService.DeleteAsync(id);
            var roleResource = mapper.Map<Role, RoleResource>(roleResponse.Role);
            var result = new ResponseData {
                Data = roleResource,
                Message = roleResponse.Message,
                Success = roleResponse.Success
            };
            return Ok(result);
        }
    }
}