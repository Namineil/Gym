using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;
        private readonly IUnitOfWork unitOfWork;
        public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            this.clientRepository = clientRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Client> GetClientAsync(int idUser) 
        {
            var client = await clientRepository.FindByIdUserAsync(idUser);
            return client;
        }

        public async Task<Client> FindByIdAsync(int id) {
            return await clientRepository.FindByIdAsync(id);
        }

        public async Task<ClientResponse> DeleteAsync(int id)
        {
            var existingClient = await clientRepository.FindByIdAsync(id);
            if (existingClient == null)
                return new ClientResponse("Клиента не найдено!");
            try
            {
                clientRepository.Remove(existingClient);
                await unitOfWork.CompleteAsync();

                return new ClientResponse(existingClient);
            }
            catch (Exception ex)
            {
                return new ClientResponse($"Ошибка при удалении Клиента: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Client>> ListAsync()
        {
            return await clientRepository.ListAsync();
        }

        public async Task<ClientResponse> SaveAsync(Client client)
        {
            try
            {
                await clientRepository.AddAsync(client);
                await unitOfWork.CompleteAsync();

                return new ClientResponse(client);
            }
            catch (Exception ex)
            {
                return new ClientResponse($"Ошибка при сохранении Клиента: {ex.Message}");
            }
        }

        public async Task<ClientResponse> UpdateAsync(int id, Client client)
        {
            var existingClient = await clientRepository.FindByIdAsync(id);
            if (existingClient == null)
                return new ClientResponse("Клиента не найдено!");
            
            existingClient.User.FullName = client.User.FullName;

            try
            {
                clientRepository.Update(existingClient);
                await unitOfWork.CompleteAsync();

                return new ClientResponse(existingClient);
            }
            catch (Exception ex)
            {
                return new ClientResponse($"Ошибка при обновлении Клиента: {ex.Message}");
            }
        }
    }
}