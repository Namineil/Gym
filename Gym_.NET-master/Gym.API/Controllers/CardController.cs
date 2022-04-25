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
    [Route("/api/card")]
    public class CardController : Controller
    {
        private readonly ICardService cardService;
        private readonly IMapper mapper;
        public CardController(ICardService cardService, IMapper mapper)
        {
            this.cardService = cardService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CardResource>> GetAllAsync() 
        {
            var card = await cardService.ListAsync();
            var resource = mapper.Map<IEnumerable<Card>,IEnumerable<CardResource>>(card);
            return resource;
        }
        
        [HttpGet("{id}")]
        public async Task<CardResource> GetCardAsync(int id) 
        {
            var card = await cardService.CardAsync(id);
            var resource = mapper.Map<Card, CardResource>(card);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCardResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var card = mapper.Map<SaveCardResource, Card>(resource);
            var result = await cardService.SaveAsync(card);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var cardResource = mapper.Map<Card, CardResource>(result.Card);
            return Ok(cardResource);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCardResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var card = mapper.Map<SaveCardResource, Card>(resource);
            var result = await cardService.UpdateAsync(id, card);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var cardResource = mapper.Map<Card, CardResource>(result.Card);
            return Ok(cardResource);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await cardService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            var cardResource = mapper.Map<Card, CardResource>(result.Card);
            return Ok(cardResource);
        }
    }
}