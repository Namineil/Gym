using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Domain.Services
{
    public interface INewsService
    {
        Task<IEnumerable<News>> ListAsync(int list);
        Task<News> NewsAsync(int id);
        Task<NewsResponse> SaveAsync(News news);
        Task<NewsResponse> UpdateAsync(int id, News news);
        Task<NewsResponse> DeleteAsync(int id);
    }
}