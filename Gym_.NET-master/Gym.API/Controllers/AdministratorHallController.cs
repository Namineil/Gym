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
    [Route("/api/administratorHall")]
    public class AdministratorHallController : Controller
    {
        private readonly IAdministratorHallService administratorHallService;
        private readonly IMapper mapper;
        public AdministratorHallController(IAdministratorHallService administratorHallService, IMapper mapper)
        {
            this.administratorHallService = administratorHallService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AdministratorHallResource>> GetAllAsync() 
        {
            var administratorHall = await administratorHallService.ListAsync();
            var resource = mapper.Map<IEnumerable<AdministratorHall>,IEnumerable<AdministratorHallResource>>(administratorHall);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveAdministratorHallResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var administratorHall = mapper.Map<SaveAdministratorHallResource, AdministratorHall>(resource);
            var result = await administratorHallService.SaveAsync(administratorHall);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var administratorHallResource = mapper.Map<AdministratorHall, AdministratorHallResource>(result.AdministratorHall);
            return Ok(administratorHallResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveAdministratorHallResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var administratorHall = mapper.Map<SaveAdministratorHallResource, AdministratorHall>(resource);
            var result = await administratorHallService.UpdateAsync(id, administratorHall);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var administratorHallResource = mapper.Map<AdministratorHall, AdministratorHallResource>(result.AdministratorHall);
            return Ok(administratorHallResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await administratorHallService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            var administratorHallResource = mapper.Map<AdministratorHall, AdministratorHallResource>(result.AdministratorHall);
            return Ok(administratorHallResource);
        }
    }
}