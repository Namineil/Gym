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
    [Route("/api/client")]
    public class ClientController : Controller
    {
        private readonly IClientService clientService;
        private readonly IMapper mapper;
        public ClientController(IClientService clientService, IMapper mapper)
        {
            this.clientService = clientService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<ClientResource>> GetAllAsync() 
        {
            var client = await clientService.ListAsync();
            var resource = mapper.Map<IEnumerable<Client>,IEnumerable<ClientResource>>(client);
            return resource;
        }

        [HttpGet("{idUser}")]
        public async Task<ClientResource> GetClientAsync(int idUser) 
        {
            var client = await clientService.GetClientAsync(idUser);
            var resource = mapper.Map<Client,ClientResource>(client);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveClientResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var client = mapper.Map<SaveClientResource, Client>(resource);
            var result = await clientService.SaveAsync(client);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var clientResource = mapper.Map<Client, ClientResource>(result.Client);
            return Ok(clientResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveClientResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var client = mapper.Map<SaveClientResource, Client>(resource);
            var result = await clientService.UpdateAsync(id, client);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var clientResource = mapper.Map<Client, ClientResource>(result.Client);
            return Ok(clientResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await clientService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            var clientResource = mapper.Map<Client, ClientResource>(result.Client);
            return Ok(clientResource);
        }
    }
}