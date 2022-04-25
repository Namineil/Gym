using Gym.API.Domain.Models;

namespace Gym.API.Domain.Services.Communication
{
    public class ClientResponse : BaseResponse
    {
        public Client Client {get; private set;}
        public ClientResponse(bool success, string message, Client client) : base(success, message)
        {
            Client = client;
        }

        public ClientResponse(Client client): this(true, string.Empty, client){}

        public ClientResponse(string message): this(false, message, null) {}
       
    }
}