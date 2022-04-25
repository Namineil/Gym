using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Domain.Services;
using Gym.API.Domain.Services.Communication;

namespace Gym.API.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository newsRepository;
        private readonly IUnitOfWork unitOfWork;
        public NewsService(INewsRepository newsRepository, IUnitOfWork unitOfWork)
        {
            this.newsRepository = newsRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<News> NewsAsync(int id) 
        {
            var news = await newsRepository.FindByIdAsync(id);
            return news;
        }

        public async Task<NewsResponse> DeleteAsync(int id)
        {
            var existingNews = await newsRepository.FindByIdAsync(id);
            if (existingNews == null)
                return new NewsResponse("Новость не найдено!");
            try
            {
                newsRepository.Remove(existingNews);
                await unitOfWork.CompleteAsync();

                return new NewsResponse(existingNews);
            }
            catch (Exception ex)
            {
                return new NewsResponse($"Ошибка при удалении Новости: {ex.Message}");
            }
        }

        public async Task<IEnumerable<News>> ListAsync(int list)
        {
            return await newsRepository.ListAsync(list);
        }

        public async Task<NewsResponse> SaveAsync(News news)
        {
            try
            {
                await newsRepository.AddAsync(news);
                await unitOfWork.CompleteAsync();

                return new NewsResponse(news);
            }
            catch (Exception ex)
            {
                return new NewsResponse($"Ошибка при сохранении Новости: {ex.Message}");
            }
        }

        public async Task<NewsResponse> UpdateAsync(int id, News news)
        {
            var existingNews = await newsRepository.FindByIdAsync(id);
            if (existingNews == null)
                return new NewsResponse("Новость не найдено!");
            
            existingNews.Title = news.Title;

            try
            {
                newsRepository.Update(existingNews);
                await unitOfWork.CompleteAsync();

                return new NewsResponse(existingNews);
            }
            catch (Exception ex)
            {
                return new NewsResponse($"Ошибка при обновлении Новости: {ex.Message}");
            }
        }
    }
}