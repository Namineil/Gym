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
    [Route("/api/cardSalesHistory")]
    public class CardSalesHistoryController : Controller
    {
        private readonly ICardSalesHistoryService cardSalesHistoryService;
        private readonly IMapper mapper;
        public CardSalesHistoryController(ICardSalesHistoryService cardSalesHistoryService, IMapper mapper)
        {
            this.cardSalesHistoryService = cardSalesHistoryService;
            this.mapper = mapper;
        }

        [HttpGet("{idUser}")]
        public async Task<CardSalesHistoryResource> GetAsync(int idUser) 
        {
            var cardSalesHistory = await cardSalesHistoryService.FindByIdUserAsync(idUser);
            var resource = mapper.Map<CardSalesHistory, CardSalesHistoryResource>(cardSalesHistory);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCardSalesHistoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var cardSalesHistory = mapper.Map<SaveCardSalesHistoryResource, CardSalesHistory>(resource);
            var result = await cardSalesHistoryService.SaveAsync(cardSalesHistory);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var cardSalesHistoryResource = mapper.Map<CardSalesHistory, CardSalesHistoryResource>(result.CardSalesHistory);
            return Ok(cardSalesHistoryResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCardSalesHistoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var cardSalesHistory = mapper.Map<SaveCardSalesHistoryResource, CardSalesHistory>(resource);
            var result = await cardSalesHistoryService.UpdateAsync(id, cardSalesHistory);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var cardSalesHistoryResource = mapper.Map<CardSalesHistory, CardSalesHistoryResource>(result.CardSalesHistory);
            return Ok(cardSalesHistoryResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await cardSalesHistoryService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            var cardSalesHistoryResource = mapper.Map<CardSalesHistory, CardSalesHistoryResource>(result.CardSalesHistory);
            return Ok(cardSalesHistoryResource);
        }
    }
}