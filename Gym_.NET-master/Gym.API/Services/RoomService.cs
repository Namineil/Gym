using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository roomRepository;
        private readonly IUnitOfWork unitOfWork;
        public RoomService(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
        {
            this.roomRepository = roomRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<RoomResponse> DeleteAsync(int id)
        {
            var existingRoom = await roomRepository.FindByIdAsync(id);
            if (existingRoom == null)
                return new RoomResponse("Зал не найдено!");
            try
            {
                roomRepository.Remove(existingRoom);
                await unitOfWork.CompleteAsync();

                return new RoomResponse(existingRoom);
            }
            catch (Exception ex)
            {
                return new RoomResponse($"Ошибка при удалении Залы: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Room>> ListAsync()
        {
            return await roomRepository.ListAsync();
        }

        public async Task<RoomResponse> SaveAsync(Room room)
        {
            try
            {
                await roomRepository.AddAsync(room);
                await unitOfWork.CompleteAsync();

                return new RoomResponse(room);
            }
            catch (Exception ex)
            {
                return new RoomResponse($"Ошибка при сохранении Залы: {ex.Message}");
            }
        }

        public async Task<RoomResponse> UpdateAsync(int id, Room room)
        {
            var existingRoom = await roomRepository.FindByIdAsync(id);
            if (existingRoom == null)
                return new RoomResponse("Зал не найдено!");
            
            existingRoom.Name = room.Name;

            try
            {
                roomRepository.Update(existingRoom);
                await unitOfWork.CompleteAsync();

                return new RoomResponse(existingRoom);
            }
            catch (Exception ex)
            {
                return new RoomResponse($"Ошибка при обновлении Залы: {ex.Message}");
            }
        }
    }
}