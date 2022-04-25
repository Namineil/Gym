using Gym.API.Domain.Models;

namespace Gym.API.Domain.Services.Communication
{
    public class NewsResponse : BaseResponse
    {
        public News News {get; private set;}
        public NewsResponse(bool success, string message, News news) : base(success, message)
        {
            News = news;
        }

        public NewsResponse(News news): this(true, string.Empty, news){}

        public NewsResponse(string message): this(false, message, null) {}
       
    }
}