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
    [Route("/api/servicesCard")]
    public class ServicesCardController : Controller
    {
        private readonly IServicesCardService servicesCardService;
        private readonly IMapper mapper;
        public ServicesCardController(IServicesCardService servicesCardService, IMapper mapper)
        {
            this.servicesCardService = servicesCardService;
            this.mapper = mapper;
        }

        [HttpGet("{idCard}")]
        public async Task<IEnumerable<ServicesCardResource>> FindByIdCardListAsync(int idCard) 
        {
            var servicesCard = await servicesCardService.FindByIdCardListAsync(idCard);
            var resource = mapper.Map<IEnumerable<ServicesCard>,IEnumerable<ServicesCardResource>>(servicesCard);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveServicesCardResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var servicesCard = mapper.Map<SaveServicesCardResource, ServicesCard>(resource);
            var result = await servicesCardService.SaveAsync(servicesCard);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var servicesCardResource = mapper.Map<ServicesCard, ServicesCardResource>(result.ServicesCard);
            return Ok(servicesCardResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveServicesCardResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var servicesCard = mapper.Map<SaveServicesCardResource, ServicesCard>(resource);
            var result = await servicesCardService.UpdateAsync(id, servicesCard);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var servicesCardResource = mapper.Map<ServicesCard, ServicesCardResource>(result.ServicesCard);
            return Ok(servicesCardResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await servicesCardService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            var servicesCardResource = mapper.Map<ServicesCard, ServicesCardResource>(result.ServicesCard);
            return Ok(servicesCardResource);
        }
    }
}