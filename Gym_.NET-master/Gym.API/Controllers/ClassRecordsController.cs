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
    [Route("/api/classRecords")]
    public class ClassRecordsController : Controller
    {
        private readonly IClassRecordsService classRecordsService;
        private readonly IMapper mapper;
        public ClassRecordsController(IClassRecordsService classRecordsService, IMapper mapper)
        {
            this.classRecordsService = classRecordsService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ClassRecordsResource>> GetAllAsync() 
        {
            var classRecords = await classRecordsService.ListAsync();
            var resource = mapper.Map<IEnumerable<ClassRecords>,IEnumerable<ClassRecordsResource>>(classRecords);
            return resource;
        }

        [HttpGet("classRecordsClient/{IdClient}")]
        public async Task<IEnumerable<ClassRecordsResource>> GetClassRecordsClientAsync(int IdClient) 
        {
            var classRecords = await classRecordsService.GetClassRecordsClientListAsync(IdClient);
            var resource = mapper.Map<IEnumerable<ClassRecords>,IEnumerable<ClassRecordsResource>>(classRecords);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveClassRecordsResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var classRecords = mapper.Map<SaveClassRecordsResource, ClassRecords>(resource);
            var result = await classRecordsService.SaveAsync(classRecords);

            if (!result.Success)
                return BadRequest(result.Message);
            var classR = await classRecordsService.FindByIdAsync(result.ClassRecords.IdClassRecords);
            
            var classRecordsResource = mapper.Map<ClassRecords, ClassRecordsResource>(classR);
            return Ok(classRecordsResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveClassRecordsResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var classRecords = mapper.Map<SaveClassRecordsResource, ClassRecords>(resource);
            var result = await classRecordsService.UpdateAsync(id, classRecords);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var classRecordsResource = mapper.Map<ClassRecords, ClassRecordsResource>(result.ClassRecords);
            return Ok(classRecordsResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await classRecordsService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            var classRecordsResource = mapper.Map<ClassRecords, ClassRecordsResource>(result.ClassRecords);
            return Ok(classRecordsResource);
        }
    }
}