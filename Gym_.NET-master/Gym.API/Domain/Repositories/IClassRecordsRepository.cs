using System.Collections.Generic;
using System.Threading.Tasks;
using Gym.API.Domain.Models;

namespace Gym.API.Domain.Repositories
{
    public interface IClassRecordsRepository
    {
        Task<IEnumerable<ClassRecords>> ListAsync();
        Task<IEnumerable<ClassRecords>> GetClassRecordsClientListAsync(int IdClient);
        Task AddAsync(ClassRecords classRecords);
        void Update(ClassRecords classRecords);
        Task<ClassRecords> FindByIdAsync(int id);
        void Remove(ClassRecords classRecords);
    }
}