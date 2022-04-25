using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;

namespace Gym.API.Persistence.Repositories
{
    public class NewsRepository : BaseRepository, INewsRepository
    {
        public NewsRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(News news)
        {
            await context.News.AddAsync(news);
        }

        public async Task<News> FindByIdAsync(int id)
        {
            return await context.News.FindAsync(id);
        }

        public async Task<IEnumerable<News>> ListAsync(int list)
        {
            return await context.News.Where(it => it.Date.CompareTo(DateTime.Now) < 0).Take(list).ToListAsync();
        }

        public void Remove(News news)
        {
            context.News.Remove(news);
        }

        public void Update(News news)
        {
            context.News.Update(news);
        }
    }
}