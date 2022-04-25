using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym.API.Domain.Models;
using Gym.API.Domain.Repositories;
using Gym.API.Persistence.Contexts;
using System.Linq;

namespace Gym.API.Persistence.Repositories
{
    public class ClassRecordsRepository : BaseRepository, IClassRecordsRepository
    {
        public ClassRecordsRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(ClassRecords classRecords)
        {
            await context.ClassRecords.AddAsync(classRecords);
        }

        public async Task<ClassRecords> FindByIdAsync(int id)
        {
            return await context.ClassRecords
                                        .Where(i=> i.IdClassRecords == id)
                                        .Include(r => r.Client)
                                        .ThenInclude(r=>r.User)
                                        .FirstAsync();
        }

        public async Task<IEnumerable<ClassRecords>> ListAsync()
        {
            return await context.ClassRecords.
                                        Include(r => r.Client)
                                        .ThenInclude(r=>r.User)
                                        .Include(t => t.ScheduleTraining)
                                        .ToListAsync();
        }

        public async Task<IEnumerable<ClassRecords>> GetClassRecordsClientListAsync(int IdClient) {
             return await context.ClassRecords
                                        .Where(c => c.IdClient == IdClient)
                                        .Include(r => r.Client)
                                        .ThenInclude(r=>r.User)
                                        .Include(t => t.ScheduleTraining)
                                        .ToListAsync();
        }

        public void Remove(ClassRecords classRecords)
        {
            context.ClassRecords.Remove(classRecords);
        }

        public void Update(ClassRecords classRecords)
        {
            context.ClassRecords.Update(classRecords);
        }
    }
}