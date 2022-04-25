using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;
using System.Linq;
using System;

namespace Gym.API.Persistence.Repositories
{
    public class ScheduleTrainingRepository : BaseRepository, IScheduleTrainingRepository
    {
        public ScheduleTrainingRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(ScheduleTraining scheduleTraining)
        {
            await context.ScheduleTraining.AddAsync(scheduleTraining);
        }

        public async Task<ScheduleTraining> FindByIdAsync(int id)
        {
            return await context.ScheduleTraining
                                        .Where(d=>d.IdTraining == id)
                                        .Include(r => r.Room)
                                        .Include(s=>s.Specialization)
                                        .Include(t=> t.Trainer)
                                        .ThenInclude(t => t.User)
                                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ScheduleTraining>> ListAsync()
        {
            return await context.ScheduleTraining
                                        .Where(d=>d.TrainingDateFrom.CompareTo(DateTime.Today) >= 0)
                                        .Include(r => r.Room)
                                        .Include(s=>s.Specialization)
                                        .Include(t=> t.Trainer)
                                        .ThenInclude(t => t.User)
                                        .ToListAsync();
        }

        public async Task<IEnumerable<ScheduleTraining>> ListIdSpecializationAsync(int idSpecialization)
        {
            return await context.ScheduleTraining
                                        .Where(sp=>sp.IdSpecialization == idSpecialization && sp.TrainingDateFrom.CompareTo(DateTime.Today) >= 0)
                                        .Include(r => r.Room)
                                        .Include(s=>s.Specialization)
                                        .Include(t=> t.Trainer)
                                        .ThenInclude(t => t.User)
                                        .ToListAsync();
        }

        public void Remove(ScheduleTraining scheduleTraining)
        {
            context.ScheduleTraining.Remove(scheduleTraining);
        }

        public void Update(ScheduleTraining scheduleTraining)
        {
            context.ScheduleTraining.Update(scheduleTraining);
        }
    }
}