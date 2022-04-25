using System.Threading.Tasks;

namespace Gym.API.Domain.Repositories {
    public interface IUnitOfWork {
        Task CompleteAsync();
    }
}