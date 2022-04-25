using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Mvc;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services;
using Gym.API.Resources;
using Gym.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Gym.API.Controllers
{
    [Authorize]
    [Route("/api/news")]
    public class NewsController : Controller
    {
        private readonly INewsService newsService;
        private readonly IMapper mapper;
        public NewsController(INewsService newsService, IMapper mapper)
        {
            this.newsService = newsService;
            this.mapper = mapper;
        }

        [HttpGet("last/{list}")]
        public async Task<IEnumerable<NewsResource>> GetAllAsync(int list) 
        {
            var news =await newsService.ListAsync(list);
            var resource = mapper.Map<IEnumerable<News>,IEnumerable<NewsResource>>(news);
            return resource;
        }
        
        [HttpGet("{id}")]
        public async Task<NewsResource> GetNewsAsync(int id) 
        {
            var news = await newsService.NewsAsync(id);
            var resource = mapper.Map<News, NewsResource>(news);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveNewsResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var news = mapper.Map<SaveNewsResource, News>(resource);
            var result = await newsService.SaveAsync(news);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var newsResource = mapper.Map<News, NewsResource>(result.News);
            return Ok(newsResource);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveNewsResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var news = mapper.Map<SaveNewsResource, News>(resource);
            var result = await newsService.UpdateAsync(id, news);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var newsResource = mapper.Map<News, NewsResource>(result.News);
            return Ok(newsResource);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await newsService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            var newsResource = mapper.Map<News, NewsResource>(result.News);
            return Ok(newsResource);
        }
    }
}