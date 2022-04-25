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
    [Route("/api/specialization")]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService specializationService;
        private readonly IMapper mapper;
        public SpecializationController(ISpecializationService specializationService, IMapper mapper)
        {
            this.specializationService = specializationService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SpecializationResource>> GetAllAsync() 
        {
            var specialization = await specializationService.ListAsync();
            var resource = mapper.Map<IEnumerable<Specialization>,IEnumerable<SpecializationResource>>(specialization);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveSpecializationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var specialization = mapper.Map<SaveSpecializationResource, Specialization>(resource);
            var result = await specializationService.SaveAsync(specialization);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var specializationResource = mapper.Map<Specialization, SpecializationResource>(result.Specialization);
            return Ok(specializationResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSpecializationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var specialization = mapper.Map<SaveSpecializationResource, Specialization>(resource);
            var result = await specializationService.UpdateAsync(id, specialization);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var specializationResource = mapper.Map<Specialization, SpecializationResource>(result.Specialization);
            return Ok(specializationResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await specializationService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            var specializationResource = mapper.Map<Specialization, SpecializationResource>(result.Specialization);
            return Ok(specializationResource);
        }
    }
}