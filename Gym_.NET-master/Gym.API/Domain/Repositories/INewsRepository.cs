using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Repositories
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> ListAsync(int list);
        Task AddAsync(News news);
        void Update(News news);
        Task<News> FindByIdAsync(int id);
        void Remove(News news);
    }
}