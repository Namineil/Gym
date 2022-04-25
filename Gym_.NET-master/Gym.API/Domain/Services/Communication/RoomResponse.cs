using Gym.API.Domain.Models;

namespace Gym.API.Domain.Services.Communication
{
    public class RoomResponse : BaseResponse
    {
        public Room Room {get; private set;}
        public RoomResponse(bool success, string message, Room room) : base(success, message)
        {
            Room = room;
        }

        public RoomResponse(Room room): this(true, string.Empty, room){}

        public RoomResponse(string message): this(false, message, null) {}
       
    }
}