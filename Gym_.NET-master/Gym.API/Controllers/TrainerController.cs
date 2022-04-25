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
    [Route("/api/trainer")]
    public class TrainerController : Controller
    {
        private readonly ITrainerService trainerService;
        private readonly IMapper mapper;
        public TrainerController(ITrainerService trainerService, IMapper mapper)
        {
            this.trainerService = trainerService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TrainerResource>> GetAllAsync() 
        {
            var trainer = await trainerService.ListAsync();
            var resource = mapper.Map<IEnumerable<Trainer>,IEnumerable<TrainerResource>>(trainer);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveTrainerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var trainer = mapper.Map<SaveTrainerResource, Trainer>(resource);
            var result = await trainerService.SaveAsync(trainer);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var trainerResource = mapper.Map<Trainer, TrainerResource>(result.Trainer);
            return Ok(trainerResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTrainerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var trainer = mapper.Map<SaveTrainerResource, Trainer>(resource);
            var result = await trainerService.UpdateAsync(id, trainer);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var trainerResource = mapper.Map<Trainer, TrainerResource>(result.Trainer);
            return Ok(trainerResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await trainerService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            var trainerResource = mapper.Map<Trainer, TrainerResource>(result.Trainer);
            return Ok(trainerResource);
        }
    }
}