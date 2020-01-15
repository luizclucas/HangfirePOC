using ConsoleAppHangfire.Domain.Entity;
using System.Threading.Tasks;

namespace ConsoleAppHangfire.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<int> SaveAsync(Client client);
    }
}
