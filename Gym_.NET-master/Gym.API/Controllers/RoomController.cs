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
    [Route("/api/room")]
    public class RoomController : Controller
    {
        private readonly IRoomService roomService;
        private readonly IMapper mapper;
        public RoomController(IRoomService roomService, IMapper mapper)
        {
            this.roomService = roomService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RoomResource>> GetAllAsync() 
        {
            var room = await roomService.ListAsync();
            var resource = mapper.Map<IEnumerable<Room>,IEnumerable<RoomResource>>(room);
            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveRoomResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var room = mapper.Map<SaveRoomResource, Room>(resource);
            var result = await roomService.SaveAsync(room);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var roomResource = mapper.Map<Room, RoomResource>(result.Room);
            return Ok(roomResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRoomResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var room = mapper.Map<SaveRoomResource, Room>(resource);
            var result = await roomService.UpdateAsync(id, room);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var roomResource = mapper.Map<Room, RoomResource>(result.Room);
            return Ok(roomResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await roomService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            
            var roomResource = mapper.Map<Room, RoomResource>(result.Room);
            return Ok(roomResource);
        }
    }
}